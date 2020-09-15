import { DataComponent } from './../data/data.component';
import { Component, OnInit } from '@angular/core';
import { GoogleBookApiService } from '../google-book-api.service';
import { ActivatedRoute,  Router } from '@angular/router';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {

book;

  constructor(private googleBooksApiService: GoogleBookApiService,
    private route: ActivatedRoute,
    private router : Router,
    private data: DataComponent
    ) {

   }

  ngOnInit() {

    let id = this.route.snapshot.params['id'];

    this.googleBooksApiService.GetBookDetails(id).
         subscribe((data) => {
            console.log(data.items);
            this.book = data;
    });
}

OnFavorite(book){

  this.googleBooksApiService.AddfavoriteBook(book);

  // this.data.storage.push(book);

  this.router.navigate(['/favorite']);
}

}
