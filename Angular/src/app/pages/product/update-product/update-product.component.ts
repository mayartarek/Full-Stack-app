import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { SweetAlertServices } from 'src/Core/Services/sweetAlert.service';
import { getCategory, getProduct } from 'src/Core/constant/api.constant';
import { catchError } from 'rxjs';
import { DataService } from 'src/Core/Services/data.service';
import { IProduct } from '../Model/product.model';

@Component({
  selector: 'app-update-product',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UpdateProductComponent {
  form: FormGroup = new FormGroup({});
  selectedFile!: File;
  imagePreview: any;
  categories: any[] = [];
  product: IProduct = {} as IProduct;
  id: any;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private router: Router,
    private route: ActivatedRoute,
    private sweetAlert: SweetAlertServices,
    private fb: FormBuilder,
    private dataService: DataService,
  ) {}

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.getProduct();
    this.getCategories();
    this.form = this.fb.group({
      name: ['', Validators.required],
      price: [0, Validators.required],
      description: [''],
      categoryId: ['', Validators.required],
      discountPercentage: [0, Validators.required],
      stock: [0, Validators.required],
    });
  }
  getCategories() {
    this.dataService
      .getList(getCategory, this.httpOptions)
      .subscribe((res: any) => {
        this.categories = res;
      });
  }
  getProduct() {
    this.dataService
      .getItemById(getProduct, `${this.id}`, this.httpOptions)
      .subscribe((res: any) => {
        this.product = res;
        this.updateForm();
        this.imagePreview = this.product.image;
        console.log(this.product);
      });
  }
  updateForm() {
    this.form.setValue({
      name: this.product.name,
      price: this.product.price,
      description: this.product.description,
      categoryId: this.product.categoryId,
      discountPercentage: this.product.discountPercentage,
      stock: this.product.stock,
    });
    this.changeDetectorRef.markForCheck();
  }
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreview = reader.result;
    };
    reader.readAsDataURL(this.selectedFile);
  }
  createProduct() {
    if (this.form?.valid) {
      const formData = new FormData();

      formData.append('name', this.form.value.name);
      formData.append('description', this.form.value.description);
      formData.append('formFile', this.selectedFile);
      formData.append('price', this.form.value.price);
      formData.append('categoryId', this.form.value.categoryId);
      formData.append('discountPercentage', this.form.value.discountPercentage);
      formData.append('stock', this.form.value.stock);
      formData.append('categoryId', this.form.value.categoryId);
      formData.append('id', this.product.id);

      this.dataService
        .post(`${this.createProduct}`, formData, this.httpOptions)
        .subscribe((res: any) => {
          if (res) {
            this.sweetAlert.successMessage();
          }
          this.router.navigate(['product/product']);
          catchError((error) => {
            this.sweetAlert.errorMessage();
            throw error;
          });
        });
    }
  }
}
