import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './modules/login/login.component';
import { RecoveryPasswordComponent } from './modules/recovery-password/recovery-password.component';

const routes: Routes = [
        {     path: '',   component:  LoginComponent    },
        {     path: 'recovery-password',      component:  RecoveryPasswordComponent}
    ];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }