import { ChangeDetectorRef, Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IProduct } from '../Model/product.model';
import { getCategory, getProduct } from 'src/Core/constant/api.constant';
import { DataService } from 'src/Core/Services/data.service';
import { SweetAlertServices } from 'src/Core/Services/sweetAlert.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ViewProductModel } from './view-product.model';

@Component({
  selector: 'app-view-product',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule],
  templateUrl: './view-product.component.html',
  styleUrls: ['./view-product.component.scss']
})
export class ViewProductComponent {
  imagePreview: any;
  categories: any[] = [];
  product: IProduct = {} as IProduct;
  id: any;  
  quantity: number = 1;
  httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
  
  constructor(
      private changeDetectorRef: ChangeDetectorRef,
      private router: Router,
      private route: ActivatedRoute,
      private sweetAlert: SweetAlertServices,
      private viewProductService: ViewProductModel,
      private dataService: DataService,
    ) {}
  
    ngOnInit() {
      this.id = this.route.snapshot.paramMap.get('id');
      this.getCategories();
      this.getProduct();}
   getProduct() {
      this.dataService
        .getItemById(getProduct, `${this.id}`, this.httpOptions)
        .subscribe((res: any) => {
          this.product = res;
          this.imagePreview = this.product.image;
          console.log(this.product);
        });
    }
    getCategories() {
        this.dataService
          .getList(getCategory, this.httpOptions)
          .subscribe((res: any) => {
            this.categories = res;
          });
      }
      addToCart() {
        console.log(this.quantity);
        let CartProduct={...this.product,quantity:this.quantity}
            console.log(CartProduct);
        this.viewProductService.addToCart(CartProduct);
        this.sweetAlert.customMessage('Product added to cart successfully!',"success");
      }
}
