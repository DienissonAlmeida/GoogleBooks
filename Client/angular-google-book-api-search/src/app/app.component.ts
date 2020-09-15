import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GoogleBookApiService } from './google-book-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  books;
  constructor(private googleBookApiService: GoogleBookApiService,
    private router : Router){

  }
  ngOnInit(): void {

  }
  OnSearch(value){

    this.router.navigateByUrl('' , { skipLocationChange: true }).then(() => {
      this.router.navigate(['/searchValue', value]);
  });
}
}
