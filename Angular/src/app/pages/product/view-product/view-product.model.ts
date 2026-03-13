import { Injectable } from '@angular/core';
import { cartProduct } from 'src/Core/Model/cartproduct.model';

@Injectable({
  providedIn: 'root',
})
export class ViewProductModel {
  /**
   *
   */

  constructor() {}

  addToCart(product: cartProduct) {
    let localCart: cartProduct[] = JSON.parse(
      localStorage.getItem('Cart') || '[]',
    );
    console.log(localCart);
    let existingProduct = localCart.find((x) => x.id === product.id);

    if (!existingProduct) {
      localCart.push(product);
    } else {
      let newQuantity = existingProduct.quantity + product.quantity;

      if (newQuantity <= product.stock) {
        existingProduct.quantity = newQuantity;
      } else {
      }
    }
    localStorage.setItem('Cart', JSON.stringify(localCart));
  }
}
