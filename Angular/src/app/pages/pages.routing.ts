import { Routes } from '@angular/router';
import { AuthGuard } from 'src/Core/Guard/auth.gurd';

export const PagesRoutes: Routes = [
  { path: '', redirectTo: 'product', pathMatch: 'full' },
  {
    path: 'auth',
    loadChildren: () =>
      import('../auth/auth.module').then((m) => m.AuthRoutes),
  },
   {
    path: 'product',
    loadChildren: () =>
      import('./product/product.module').then((m) => m.ProductRoutes),
    canActivate:[AuthGuard]
  },
]