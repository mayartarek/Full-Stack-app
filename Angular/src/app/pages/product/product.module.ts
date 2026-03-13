import { Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { CreateProductComponent } from './create-product/create-product.component';
import { UpdateProductComponent } from './update-product/update-product.component';
import { ViewProductComponent } from './view-product/view-product.component';

export const ProductRoutes: Routes = [
  { path: '', redirectTo: 'product', pathMatch: 'full' },

  {
    path: 'product',
    component: ProductListComponent,
  },
  {
    path: 'create',
    component: CreateProductComponent,
  },
  {
    path: 'update/:id',
    component: UpdateProductComponent,
  },
   {
    path: 'view/:id',
    component: ViewProductComponent,
  }
];
