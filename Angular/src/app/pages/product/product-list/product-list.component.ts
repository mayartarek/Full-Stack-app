import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IProduct } from '../Model/product.model';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DataService } from 'src/Core/Services/data.service';
import { getAllProduct } from 'src/Core/constant/api.constant';
import { AuthService } from 'src/Core/Services/auth.service';
import { StorageService } from 'src/Core/Services/stoge.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent {
    searchText = '';
 totalCount = 0;
  pageNumber = 1;
  pageSize = 10;
  pages: number[] = [];
    totalPages = 0; 
    isAdmin=false;
  constructor(private router: Router,private dataService: DataService,private storageService:StorageService) { }
public items: IProduct[]=[];
ngOnInit(){
  this.getProduct();
  const userJson = this.storageService.getStorage('user');
  let userInfo: any = null;
  if (userJson) {
    try {
      userInfo = JSON.parse(userJson);
    } catch (e) {
      console.error('Failed to parse user from storage', e);
      userInfo = null;
    }
  }
  this.isAdmin = userInfo?.roles === 'Admin';
  console.log(this.isAdmin);
}

addItem(){
  this.router.navigate(['product/create']);
}
getProduct(){
  this.dataService.getList(`${getAllProduct}`+`?PageNumber=${this.pageNumber}&PageSize=${this.pageSize}`).subscribe((res:any)=>{
    this.items = res.list;
 this.totalCount = res.totalCount;

      this.totalPages = (this.totalCount + this.pageSize - 1) / this.pageSize;

      this.pages = Array.from({ length: Math.floor(this.totalPages) }, (_, i) => i + 1);  });
}
view(item: IProduct){
  this.router.navigate(['product/view',item.id]); 
}
edit(item: IProduct){
  this.router.navigate(['product/update',item.id]);
}
getAllProduct(event: any){
  this.dataService.getList(`${getAllProduct}`+`?PageNumber=${this.pageNumber}&PageSize=${this.pageSize}`).subscribe((res:any)=>{
    this.items = res.list;
    console.log(this.items);
  });
}

  onPageChange(page: number) {
    this.pageNumber = page;
    this.getProduct();
  }
}