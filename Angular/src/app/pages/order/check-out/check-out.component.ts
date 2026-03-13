import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { createOrder } from 'src/Core/constant/api.constant';
import { DataService } from 'src/Core/Services/data.service';
import { catchError } from 'rxjs';
import { SweetAlertServices } from 'src/Core/Services/sweetAlert.service';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-check-out',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.scss'],
})
export class CheckOutComponent {
  cartItems: any[] = [];
  form: FormGroup = new FormGroup({});

     httpOptions = {
        headers: new HttpHeaders({ "Content-Type": "application/json" }),
    };
    
  constructor(private fb: FormBuilder,private dataService: DataService,private sweetAlert:SweetAlertServices,private router: Router) {}
  ngOnInit() {
    this.loadCart();
    this.form = this.fb.group({
      shippingAddress: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
    });
  }

  loadCart() {
    this.cartItems = JSON.parse(localStorage.getItem('Cart') || '[]');
  }
  removeItem(item: any) {
    this.cartItems = this.cartItems.filter(
      (x) =>
        !(
          x.id === item.id
        ),
    );

    localStorage.setItem('Cart', JSON.stringify(this.cartItems));
  }
  getTotal() {
    return this.cartItems.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0,
    );
  }
  createOrder() {
    let orderItem: any[] = [];
    this.cartItems.forEach((element) => {
      orderItem.push({ productId: element.id, quantity: element.quantity });
    });
    let Order = {
      orderItem: orderItem,
      createUserDetails: this.form.value,
    };
    this.dataService.post(`${createOrder}`,Order,this.httpOptions).subscribe((res:any)=>{
      if(res){
        this.sweetAlert.successMessage();
       localStorage.removeItem('Cart');
      }
      this.router.navigate(['order/Order-list']);
      catchError((error) => {
        this.sweetAlert.errorMessage();
        throw error;
      });
    });
  }
}
