import { Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';

export const ProductRoutes: Routes = [
  { path: '', redirectTo: 'product', pathMatch: 'full' },

  {
    path: 'product',
    component: ProductListComponent,
  },
];
