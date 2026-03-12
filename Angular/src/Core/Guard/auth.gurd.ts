import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { CoreService } from "../Services/core.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard  {

    constructor(private _coreService: CoreService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        const tokenExists = this._coreService.getCookie('access_token');

        if (!tokenExists) {
            const queryParams = {
                redirectUrl: state.url,
            };
            this.router.navigate(["/auth/signin"], {
                queryParams,
            });
            return false;
        }

        return true;
    }

    isExpired(expireIn: any) {
        return expireIn < (Date.now() / 1000);
    }

}