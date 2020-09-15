import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GoogleBookApiService } from '../google-book-api.service';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  books;

  constructor(private googleBookApiService: GoogleBookApiService,
    private route: ActivatedRoute,) { }

  OnSearch(s){

  }

  ngOnInit() {

    let value = this.route.snapshot.params['value'];

    this.googleBookApiService.GetBooksCatalog(value)
        .subscribe((data) => {
            this.books = data;
        });
  }

}
