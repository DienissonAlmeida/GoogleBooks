import { RouterModule , Routes } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { HomeComponent } from './home/home.component';
import { ResultComponent } from './result/result.component';
import { SearchComponent } from './search/search.component';
import { FavoriteComponent } from './favorite/favorite.component';


const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'search',  component: SearchComponent },
    { path: 'searchId/:id', component: ResultComponent },
    { path: 'searchValue/:value', component: SearchComponent },
    { path: 'favorite', component: FavoriteComponent },
    // { path: 'result/:value', component: ResultComponent },
    { path: '**', component: NotFoundComponent }
]

export const AppRouting = RouterModule.forRoot(appRoutes);
