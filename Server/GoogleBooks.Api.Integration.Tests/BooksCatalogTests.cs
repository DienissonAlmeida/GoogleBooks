﻿using AutoMapper;
using GoogleBooks.Domain.Dtos.Output;
using GoogleBooks.Domain.Dtos.Output.Exceptions;
using GoogleBooks.Domain.Helpers;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Api.Services;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NFluent;
using System;
using System.Collections.Generic;
using Xunit;

namespace GoogleBooks.Api.Integration.Tests
{
    public class BooksCatalogTests : TestFactory
    {
        private IBooksService _bookService;
        private Mock<IGoogleBooksClientService> _mockedGoogleClientService;
        private Mock<IMapper> _mockedMapperService;
        private readonly ILogger<BooksService> _logger;

        public BooksCatalogTests()
        {
            _mockedGoogleClientService = MockService<IGoogleBooksClientService>();
            _mockedMapperService = MockService<IMapper>();
            _logger = CreateLogger<BooksService>();
        }

        [Fact(DisplayName = "Should get 5 books from catalog")]
        public async void Should_GetFiveBooksFromCatalog()
        {
            // Prepare
            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var kind = "Test Kind";
            var googleClientResult = new GoogleBooksCatalog
            {
                Kind = kind,
                TotalItems = 5,
                Items = new GoogleBookDetailsLite[]
                {
                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 1",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 1",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 1,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 2",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 2",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 2,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 3",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 3",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 3,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 4",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 4",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 4,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 5",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 5",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 5,
                        },
                    }
                }
            };
            _mockedGoogleClientService.Setup(s => s.GetBooksCatalogAsync(new Domain.Domain.BooksCatalog(keywords, pageSize, pageNumber))).ReturnsAsync(googleClientResult);

            var mapperServiceResult = new List<BookDetailsForCatalog>
            {
                new BookDetailsForCatalog
                {
                    Country = "Test Country 1",
                    AccessViewStatus = "Test AccessViewStatus",
                    QuoteSharingAllowed = "Test AccessViewStatus",
                    TextToSpeechPermission = "Test TextToSpeechPermission",
                    WebReaderLink = "Test WebReaderLink",
                    Kind = "Test Kind",
                    SelfLink = "Test SelfLink",
                    Authors = new string[] { "Test Author" },
                    CanonicalVolumeLink = "Test CanonicalVolumeLink",
                    Description = "Test Description 1",
                    Categories = new string[] { "Test Category" },
                    InfoLink = "Test InfoLink",
                    Language = "Test Languge",
                    PageCount = 1
                },

                new BookDetailsForCatalog
                {
                    Country = "Test Country 2",
                    AccessViewStatus = "Test AccessViewStatus",
                    QuoteSharingAllowed = "Test AccessViewStatus",
                    TextToSpeechPermission = "Test TextToSpeechPermission",
                    WebReaderLink = "Test WebReaderLink",
                    Kind = "Test Kind",
                    SelfLink = "Test SelfLink",
                    Authors = new string[] { "Test Author" },
                    CanonicalVolumeLink = "Test CanonicalVolumeLink",
                    Description = "Test Description 2",
                    Categories = new string[] { "Test Category" },
                    InfoLink = "Test InfoLink",
                    Language = "Test Languge",
                    PageCount = 2
                },

                new BookDetailsForCatalog
                {
                    Country = "Test Country 3",
                    AccessViewStatus = "Test AccessViewStatus",
                    QuoteSharingAllowed = "Test AccessViewStatus",
                    TextToSpeechPermission = "Test TextToSpeechPermission",
                    WebReaderLink = "Test WebReaderLink",
                    Kind = "Test Kind",
                    SelfLink = "Test SelfLink",
                    Authors = new string[] { "Test Author" },
                    CanonicalVolumeLink = "Test CanonicalVolumeLink",
                    Description = "Test Description 3",
                    Categories = new string[] { "Test Category" },
                    InfoLink = "Test InfoLink",
                    Language = "Test Languge",
                    PageCount = 3
                },

                new BookDetailsForCatalog
                {
                    Country = "Test Country 4",
                    AccessViewStatus = "Test AccessViewStatus",
                    QuoteSharingAllowed = "Test AccessViewStatus",
                    TextToSpeechPermission = "Test TextToSpeechPermission",
                    WebReaderLink = "Test WebReaderLink",
                    Kind = "Test Kind",
                    SelfLink = "Test SelfLink",
                    Authors = new string[] { "Test Author" },
                    CanonicalVolumeLink = "Test CanonicalVolumeLink",
                    Description = "Test Description 4",
                    Categories = new string[] { "Test Category" },
                    InfoLink = "Test InfoLink",
                    Language = "Test Languge",
                    PageCount = 4
                },

                new BookDetailsForCatalog
                {
                    Country = "Test Country 5",
                    AccessViewStatus = "Test AccessViewStatus",
                    QuoteSharingAllowed = "Test AccessViewStatus",
                    TextToSpeechPermission = "Test TextToSpeechPermission",
                    WebReaderLink = "Test WebReaderLink",
                    Kind = "Test Kind",
                    SelfLink = "Test SelfLink",
                    Authors = new string[] { "Test Author" },
                    CanonicalVolumeLink = "Test CanonicalVolumeLink",
                    Description = "Test Description 5",
                    Categories = new string[] { "Test Category" },
                    InfoLink = "Test InfoLink",
                    Language = "Test Languge",
                    PageCount = 5
                }
            };
            _mockedMapperService.Setup(s => s.Map<List<BookDetailsForCatalog>>(googleClientResult.Items)).Returns(mapperServiceResult);

            var booksCatalogPaging = new PagingCatalogResult
            (
                keywords,
                pageNumber,
                pageSize,
                5
            );
            var expectedResult = new BooksCatalogResult
            (
                new GoogleBooks.Domain.Dtos.Output.BooksCatalogSearchResult
                (
                    booksCatalogPaging,
                    new BooksCatalog(kind, mapperServiceResult)
                ), StatusEnum.Ok);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            var booksCatalogParameter = new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageNumber, pageSize);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.PagingInfo.Keywords).Equals(actualResult.PagingInfo.Keywords);
            Check.That(expectedResult.PagingInfo.PageNumber).Equals(actualResult.PagingInfo.PageNumber);
            Check.That(expectedResult.PagingInfo.PageSize).Equals(actualResult.PagingInfo.PageSize);
            Check.That(expectedResult.PagingInfo.TotalItems).Equals(actualResult.PagingInfo.TotalItems);

            for (var i = 0; i < actualResult.BooksCatalog.BookDetails.Count; i++)
            {
                Check.That(expectedResult.BooksCatalog.BookDetails[i]).Equals(actualResult.BooksCatalog.BookDetails[i]);
            }
        }

        [Fact(DisplayName = "Should respond with an invalid parameter exception because of null 'booksCatalogSearch' argument")]
        public async void Should_RespondInvalidParameterException()
        {
            // Prepare
            GoogleBooks.Domain.Domain.BooksCatalog booksCatalogParameter = null;

            var expectedResult = new IndividualBookDetailsResult(new InvalidBookException(ExceptionMessages.NullArgument), StatusEnum.InvalidParamater);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.Error.Data).Equals(actualResult.Error.Data);
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }

        [Fact(DisplayName = "Should respond with an empty catalog because keywords were not found")]
        public async void Should_RespondEmptyCatalogWhenKeywordsNotFound()
        {
            // Prepare
            var kind = "Test Kind";

            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var booksCatalogPaging = new PagingCatalogResult
            (
                keywords,
                pageNumber,
                pageSize,
                0
            );
            var expectedResult = new BooksCatalogResult
            (
                new BooksCatalogSearchResult
                (
                    booksCatalogPaging,
                    new BooksCatalog(kind, new List<BookDetailsForCatalog>())
                ), StatusEnum.Ok);

            var googleClientResult = new GoogleBooksCatalog
            {
                Kind = kind,
                Items = null,
                TotalItems = 0
            };
            
            _mockedGoogleClientService.Setup(s => s.GetBooksCatalogAsync(new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageSize, pageNumber)))
                .ReturnsAsync(googleClientResult);

            var booksCatalogParameter = new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageNumber, pageSize);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.BooksCatalog.BookDetails).Equals(actualResult.BooksCatalog.BookDetails);
            Check.That(expectedResult.BooksCatalog.Kind).Equals(actualResult.BooksCatalog.Kind);
            Check.That(expectedResult.PagingInfo.Keywords).Equals(actualResult.PagingInfo.Keywords);
            Check.That(expectedResult.PagingInfo.PageNumber).Equals(actualResult.PagingInfo.PageNumber);
            Check.That(expectedResult.PagingInfo.PageSize).Equals(actualResult.PagingInfo.PageSize);
            Check.That(expectedResult.PagingInfo.TotalItems).Equals(actualResult.PagingInfo.TotalItems);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the google client failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnGoogleClient()
        {
            // Prepare
            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var googleClientResult = new GoogleBooksCatalog();

            var expectedResult = new BooksCatalogResult
            (
                new InternalServerException("Google client unexpected exception"),
                StatusEnum.InternalError
            );

            _mockedGoogleClientService
                .Setup(s => s.GetBooksCatalogAsync(new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageSize, pageNumber)))
                .Throws(new Exception("Google client unexpected exception"));

            var booksCatalogParameter = new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageSize, pageNumber);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(actualResult.Error).IsInstanceOf<InternalServerException>();
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the mapper service failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnMapperService()
        {
            // Prepare
            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var googleClientResult = new GoogleBooksCatalog
            {
                Kind = "Test Kind",
                TotalItems = 1,
                Items = new GoogleBookDetailsLite[]
                {
                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 1",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 1",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 1,
                        },
                    }
                }
            };

            var expectedResult = new BooksCatalogResult
            (
                new InternalServerException("Mapper service unexpected exception"),
                StatusEnum.InternalError
            );

            _mockedGoogleClientService
                .Setup(s => s.GetBooksCatalogAsync(new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageSize, pageNumber)))
                .ReturnsAsync(googleClientResult);

            _mockedMapperService
                .Setup(s => s.Map<List<BookDetailsForCatalog>>(googleClientResult.Items))
                .Throws(new Exception("Mapper service unexpected exception"));

            var booksCatalogParameter = new GoogleBooks.Domain.Domain.BooksCatalog(keywords, pageSize, pageNumber);
            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(actualResult.Error).IsInstanceOf<InternalServerException>();
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }
    }
}
