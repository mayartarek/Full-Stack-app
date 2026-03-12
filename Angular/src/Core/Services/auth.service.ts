import { Injectable, signal, WritableSignal } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { CoreService } from "./core.service";
import { Router } from "@angular/router";

@Injectable({
    providedIn: "root",
})

export class AuthService {

    IsAuth: WritableSignal<boolean> = signal(false);
    userInfo: BehaviorSubject<any> = new BehaviorSubject({});
    constructor( private _coreService: CoreService, private Router: Router) { }

logOut() {
     this._coreService.removeAllCookies();
                this.Router.navigate(['auth/signin']);
                this.IsAuth.set(false);
}}
