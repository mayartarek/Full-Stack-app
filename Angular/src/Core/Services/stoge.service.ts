import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class StorageService {

    // baseUrl = baseUrl;

    getStorage(key: any) { // get LocalStorage
        return localStorage.getItem(key);
    }

    setStorage(key: string, value: any) { // create LocalStorage
        return localStorage.setItem(key, value);
    }

    removeStorage(key: string) { // Remove LocalStorage By key
        return localStorage.removeItem(key);
    }

    removeKeysStorage(keys: string[]) {
        keys.forEach((key) => this.removeStorage(key));
    }

    // Check on key in localStorage
    existsStorage(key: string) {
        return !!localStorage.getItem(key); // return true or false
    }

    clearStorage() {
        return localStorage.clear();
    }

    setAccessToken(value: any) {
        // var tokenInfo = this.getDecodedAccessToken(value); // decode token
        // this.setUser(tokenInfo);
        return this.setStorage('access_token', value);
    }

    setUser(value: any) {
        return this.setStorage('user', JSON.stringify(value));
    }
    setLanguage(value: any) {
        return this.setStorage('language', JSON.stringify(value));
    }

    setExpiresIn(value: any) {
        return this.setStorage('expires_in', value);
    }

    getAccessToken() {
        return this.getStorage('access_token');
    }

    getExpiresIn() {
        return this.getStorage('expires_in');
    }

    // getDecodedAccessToken(token: string): any {
    //     try {
    //         return jwt_decode(token);
    //     } catch (Error) {
    //         return null;
    //     }
    // }

}
