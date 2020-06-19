import { Component } from '@angular/core';

import { ShareAuthService } from 'src/app/share/services/share-auth-service';
import { IdentityService } from 'src/app/services/identity/identity.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent {
  public isLogin: boolean;

  constructor(
    public shareAuthService: ShareAuthService,
    public identityService: IdentityService,
    public router: Router) { 
    this.shareAuthService.data.subscribe(data => {
      this.isLogin = data;
    })
  }

  public logout() {
    this.identityService.logout();
    this.shareAuthService.updatedDataSelection(this.identityService.isLoggedIn());
    this.router.navigate(['/home']);
  }
}
