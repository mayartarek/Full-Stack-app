import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from 'src/Core/Services/auth.service';
import { DataService } from 'src/Core/Services/data.service';
import { createProduct, getCategory } from 'src/Core/constant/api.constant';
import { HttpHeaders } from '@angular/common/http';
import { SweetAlertServices } from 'src/Core/Services/sweetAlert.service';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-create-product',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule,ReactiveFormsModule],
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent {
form: FormGroup=new FormGroup({});
 selectedFile!: File;
  imagePreview: any;
  categories: any[] = [];

   httpOptions = {
      headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };
  
constructor(private router: Router,private sweetAlert:SweetAlertServices,private fb :FormBuilder,private dataService: DataService) { }

ngOnInit(){
  this.getCategories();
  this.form = this.fb.group({
    name: ['',Validators.required],
    price: [0,Validators.required],
    description: [''],
    categoryId: ['',Validators.required],
    discountPercentage: [0,Validators.required],
    stock: [0,Validators.required]
  });
}
getCategories(){
  this.dataService.getList(getCategory,this.httpOptions).subscribe((res:any)=>{
    this.categories = res;
  });
}

onFileSelected(event: any) {
  this.selectedFile = event.target.files[0];
  const reader = new FileReader();
  reader.onload = () => {
    this.imagePreview = reader.result;
  };
  reader.readAsDataURL(this.selectedFile);
}
createProduct(){
  if (this.form?.valid) {
   
    const formData = new FormData();

    formData.append("name",this.form.value.name);
    formData.append("description",this.form.value.description);
    formData.append("formFile",this.selectedFile);
        formData.append("price",this.form.value.price);
        formData.append("categoryId",this.form.value.categoryId);
        formData.append("discountPercentage",this.form.value.discountPercentage);
        formData.append("stock",this.form.value.stock);
        formData.append("categoryId",this.form.value.categoryId);

this.dataService.post(`${createProduct}`,formData,this.httpOptions).subscribe((res:any)=>{
  if(res){
    this.sweetAlert.successMessage();

  }
  this.router.navigate(['product/product']);
  catchError((error) => {
    this.sweetAlert.errorMessage();
    throw error;
  });
});
  }}}