﻿using AutoMapper;
using GoogleBooks.Domain.Domain;
using GoogleBooks.Domain.Dtos;
using GoogleBooks.Domain.Dtos.Output;
using GoogleBooks.Domain.Dtos.Output.Exceptions;
using GoogleBooks.Domain.Helpers;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainBooksCatalog = GoogleBooks.Domain.Domain.BooksCatalog;
using DtosBooksCatalog = GoogleBooks.Domain.Dtos.Output.BooksCatalog;

namespace GoogleBooks.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksService> _logger;

        public BooksService
        (
            IGoogleBooksClientService googleBooksClientService,
            IMapper mapper,
            ILogger<BooksService> logger
        )
        {
            _googleBooksClientService = googleBooksClientService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IndividualBookDetailsResult> GetBookDetailsAsync(Book book)
        {
            try
            {
                if (book == null)
                    return new IndividualBookDetailsResult(new InvalidBookException(ExceptionMessages.NullArgument), StatusEnum.InvalidParamater);

                var individualBookDetails = await _googleBooksClientService.GetBookDetailsAsync(book.Id);
                if (individualBookDetails == null)
                {
                    return new IndividualBookDetailsResult
                    (
                        new NotFoundException(ExceptionMessages.GetNotFoundMessage(book.Id)), StatusEnum.NotFound
                    );
                }

                IndividualBookDetails bookDetails = _mapper.Map<IndividualBookDetails>(individualBookDetails);

                return new IndividualBookDetailsResult(bookDetails, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return new IndividualBookDetailsResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<BooksCatalogResult> GetBooksCatalogAsync(DomainBooksCatalog booksCatalogSearch)
        {
            try
            {
                if (booksCatalogSearch == null)
                    return new BooksCatalogResult(new InvalidBookException(ExceptionMessages.NullArgument), StatusEnum.InvalidParamater);


                var booksCatalogResult = await _googleBooksClientService.GetBooksCatalogAsync(booksCatalogSearch);

                var booksCatalogPaging = new PagingCatalogResult
                (
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageNumber,
                    booksCatalogSearch.PageSize,
                    booksCatalogResult.TotalItems
                );

                if (booksCatalogResult.Items == null)
                {
                    return new BooksCatalogResult
                    (
                        new BooksCatalogSearchResult
                        (
                            booksCatalogPaging,
                            new DtosBooksCatalog
                            (
                                booksCatalogResult.Kind,
                                new List<BookDetailsForCatalog>()
                            )
                        ),
                        StatusEnum.Ok
                    );
                }

                List<BookDetailsForCatalog> bookDetails = _mapper.Map<List<BookDetailsForCatalog>>(booksCatalogResult.Items);

                var booksCatalog = new DtosBooksCatalog(booksCatalogResult.Kind, bookDetails);
                var booksCatalogSearchResult = new BooksCatalogSearchResult(booksCatalogPaging, booksCatalog);

                return new BooksCatalogResult(booksCatalogSearchResult, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return new BooksCatalogResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }
    }
}
