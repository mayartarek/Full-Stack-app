import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class CoreService {
  getCookie(key: string) {
    const nameEQ = key + '=';
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1, c.length);
      }
      if (c.indexOf(nameEQ) === 0) {
        return decodeURIComponent(c.substring(nameEQ.length));
      }
    }
    return null;
  }
  removeAllCookies(): void {
    const allCookies = this.getAllCookies();

    for (const key in allCookies) {
      this.deleteCookie(key);
    }
  }
  deleteCookie(key: string): void {
    document.cookie = `${key}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  }
getAllCookies(): { [key: string]: string } {
    const cookies = document.cookie.split(';');
    const allCookies: { [key: string]: string } = {};

    for (const cookie of cookies) {
      const [key, value] = cookie.trim().split('=');
      allCookies[key] = decodeURIComponent(value);
    }

    return allCookies;
  }
  setCookie(name: string, value: any, days?: number): void {
    let expires = '';
    if (days) {
      const d = new Date();
      d.setTime(d.getTime() + days * 24 * 60 * 60 * 1000);
      expires = '; expires=' + d.toUTCString();
    }
    document.cookie =
      name + '=' + encodeURIComponent(value) + expires + '; path=/';
  }

  getDecodedAccessToken(token: string): any {
    if (token) {
      try {
        debugger;
        // Decode the JWT token
        const decodedToken = jwtDecode(token);
        return decodedToken;
      } catch (error) {
        console.error('Invalid token:', error);
        return null;
      }
    }

    return null;
  }
}
