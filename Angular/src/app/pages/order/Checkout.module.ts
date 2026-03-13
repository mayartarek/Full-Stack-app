import { Routes } from "@angular/router";
import { CheckOutComponent } from "./check-out/check-out.component";
import { OrderListComponent } from "./order-list/order-list.component";

export const CheckOutRoutes: Routes = [
  { path: '', component: CheckOutComponent },
  { path: 'Order-list', component: OrderListComponent },
]
