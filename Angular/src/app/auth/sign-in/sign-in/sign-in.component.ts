import { Component, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { IUser } from '../model/user';
import { DataService } from 'src/Core/Services/data.service';
import { CoreService } from 'src/Core/Services/core.service';
import { AuthService } from 'src/Core/Services/auth.service';
import { StorageService } from 'src/Core/Services/stoge.service';
import { signIn } from 'src/Core/constant/api.constant';
import { HttpHeaders } from '@angular/common/http';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterModule],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  password: any;
  show = false;
  public CustomControler: any;
  returnUrl!: string;
  loginForm!: FormGroup;
  token!: string;
  userInfo: any;

 httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
};

 
  constructor(
    private _dataService: DataService,
    private _coreService: CoreService,
    public destroyRef: DestroyRef,
    public _router: Router,
    public authService: AuthService,
    public router: Router,
    public route: ActivatedRoute,
    private fb: FormBuilder,
    private storageService:StorageService
  ) {
    this.returnUrl =
      this.route.snapshot.queryParamMap.get('redirectUrl') || '/';
  }
  ngOnInit() {
    this.createSignInForm();
    this.password = "password";
  }

  createSignInForm() {
    this.loginForm = this.fb.group({
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required]),
    });
  }

  onClick() {
    if (this.password === "password") {
      this.password = "text";
      this.show = true;
    } else {
      this.password = "password";
      this.show = false;
    }
  }
  FireLogin(){
    this.loginHandler(this.loginForm.value)
  }
loginHandler(user:IUser){
  console.log("fire")
this._dataService.post(`${signIn}`,user ,this.httpOptions)
.pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
        next: (response: any) => {
          if (response['token']) {
            this._coreService.setCookie('access_token', response['token']);
          
            const token = this._coreService.getCookie('access_token');
            this.userInfo = this._coreService.getDecodedAccessToken(
              response['token']
            );
            this.authService.userInfo.next(this.userInfo);
            this.storageService.setUser(this.userInfo);

            this.authService.IsAuth.set(true);
            this.router.navigateByUrl(this.returnUrl);
          }
        },
        error: (error: any) => {
          this.router.navigateByUrl('auth/signin');
        },
      })
}
}
