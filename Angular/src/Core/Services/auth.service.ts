import { Injectable, signal, WritableSignal } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";
import { CoreService } from "./core.service";

@Injectable({
    providedIn: "root",
})

export class AuthService {

    IsAuth: WritableSignal<boolean> = signal(false);
    userInfo: BehaviorSubject<any> = new BehaviorSubject({});


}