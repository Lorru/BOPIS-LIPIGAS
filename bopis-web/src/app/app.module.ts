import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
 
import { AppComponent } from './app.component';
import { LoginComponent } from './modules/login/login.component';
import { RecoveryPasswordComponent } from './modules/recovery-password/recovery-password.component';
import { AppRoutingModule } from './app-routing.module';
import { MenuModule } from './modules/menu/menu.module';
import { FormLoginComponent } from './components/form/form-login/form-login.component';
import { FormRecoveryPasswordComponent } from './components/form/form-recovery-password/form-recovery-password.component';
import { ModalAttendOrderComponent } from './components/modal/modal-attend-order/modal-attend-order.component';
import { ModalDetailOrderComponent } from './components/modal/modal-detail-order/modal-detail-order.component';
import { ModalCancelOrderComponent } from './components/modal/modal-cancel-order/modal-cancel-order.component';

import { ToastrModule } from 'ngx-toastr';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { ModalAddOrderComponent } from './components/modal/modal-add-order/modal-add-order.component';
import { ModalEditLocalComponent } from './components/modal/modal-edit-local/modal-edit-local.component';
import { ModalAddLocalComponent } from './components/modal/modal-add-local/modal-add-local.component';
import { ModalChangePasswordUserComponent } from './components/modal/modal-change-password-user/modal-change-password-user.component';
import { ModalEditUserComponent } from './components/modal/modal-edit-user/modal-edit-user.component';
import { ModalAddUserComponent } from './components/modal/modal-add-user/modal-add-user.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RecoveryPasswordComponent,
    FormLoginComponent,
    FormRecoveryPasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MenuModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-center',
    }),
    NgbModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents:[
    ModalAttendOrderComponent,
    ModalDetailOrderComponent,
    ModalCancelOrderComponent,
    ModalAddOrderComponent,
    ModalEditLocalComponent,
    ModalAddLocalComponent,
    ModalChangePasswordUserComponent,
    ModalEditUserComponent,
    ModalAddUserComponent
  ]
})
export class AppModule { }
