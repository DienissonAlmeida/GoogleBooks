import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

@Injectable()
export class GoogleBookApiService {

  constructor(private httpClient: Http) { }


  GetBookDetails(isbn) {

    const encodedURI = encodeURI("https://localhost:5001/Books/GetBookDetails?bookId=" + isbn);

    return this.httpClient.get(encodedURI)
      .map((response: Response) => response.json());
  }

  GetBooksCatalog(search) {
    const encodedURI = encodeURI("https://localhost:5001/Books/GetBooksCatalog")
    const params = {
      "keywords": search,
      "pageSize": 40,
      "pageNumber": 0
    };
    return this.httpClient.post(encodedURI, params)
      .map((response: Response) => response.json());
  }

  AddfavoriteBook(book: any) {
    const encodedURI = encodeURI("https://localhost:5001/Books/GetBooksCatalog")
    const params = {
      "keywords": book,
      "pageSize": 40,
      "pageNumber": 0
    };
    return this.httpClient.post(encodedURI, params)
      .map((response: Response) => response.json());
  }

}
