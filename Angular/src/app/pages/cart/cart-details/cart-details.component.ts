import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart-details',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule],
  templateUrl: './cart-details.component.html',
  styleUrls: ['./cart-details.component.scss']
})
export class CartDetailsComponent {
 cartItems: any[] = [];
constructor(private router: Router) { } 
  ngOnInit() {
    this.loadCart();
  }

  loadCart() {
    this.cartItems = JSON.parse(localStorage.getItem('Cart') || '[]');
  }

  removeItem(item: any) {

    this.cartItems = this.cartItems.filter(
      x =>
        !(x.productId === item.productId &&
          x.productSizeId === item.productSizeId)
    );

    localStorage.setItem('Cart', JSON.stringify(this.cartItems));
  }

  clearCart() {
    localStorage.removeItem('Cart');
    this.cartItems = [];
  }

  getTotal() {
    return this.cartItems.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );
  }
  Checkout(){
    this.router.navigate(['/order']);
  }
}
