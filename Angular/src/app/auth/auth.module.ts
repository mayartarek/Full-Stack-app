import { Routes } from "@angular/router";
import { SignInComponent } from "./sign-in/sign-in/sign-in.component";
import { AuthGuard } from "src/Core/Guard/auth.gurd";
import { guestGuard } from "src/Core/Guard/guest.guard";

    export const AuthRoutes: Routes = [

    {
        path: 'signin',
        component: SignInComponent,
        canActivate:[guestGuard]

    }]
