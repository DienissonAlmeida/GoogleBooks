import { DataComponent } from './../data/data.component';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private data: DataComponent) { }

  books = Array<any>();

  ngOnInit() {
    this.books = this.data.storage;
  }

  OnExclude(id) {

    let isConfirm = confirm("Deseja realmente excluir?")

    if (isConfirm) {
      for (var i = 0; i < this.books.length; i++) {
        var obj = this.books[i];

        var indexOf = this.books.indexOf(obj.id);

        if (obj.id === id) {
          this.books.splice(indexOf, 1);
        }
      }
    }
    else return;

  }

}
