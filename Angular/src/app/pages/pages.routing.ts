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
  {
    path: 'cart',
    loadChildren: () =>
      import('./cart/cart.module').then((m) => m.CartRoutes),
    canActivate:[AuthGuard]
  },
   {
    path: 'order',
    loadChildren: () =>
      import('./order/Checkout.module').then((m) => m.CheckOutRoutes),
    canActivate:[AuthGuard]
  },
]