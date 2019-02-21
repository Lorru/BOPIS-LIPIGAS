import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MenuRoutingModule } from './menu-routing.module';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from '../home/home.component';
import { SearcherComponent } from '../searcher/searcher.component';
import { OptionsComponent } from '../options/options.component';
import { PricesComponent } from '../prices/prices.component';
import { LocalsComponent } from '../locals/locals.component';
import { UsersComponent } from '../users/users.component';
import { ReportsComponent } from '../reports/reports.component';
import { ConfigurationsComponent } from '../configurations/configurations.component';
import { MenuComponent } from './menu.component';
import { TableHomeComponent } from 'src/app/components/table/table-home/table-home.component';
import { TableSearcherComponent } from 'src/app/components/table/table-searcher/table-searcher.component';
import { TableLocalsComponent } from 'src/app/components/table/table-locals/table-locals.component';
import { TableUsersComponent } from 'src/app/components/table/table-users/table-users.component';
import { TableReportsComponent } from 'src/app/components/table/table-reports/table-reports.component';
import { GridHomeComponent } from 'src/app/components/grid/grid-home/grid-home.component';
import { GridOptionsComponent } from 'src/app/components/grid/grid-options/grid-options.component';
import { GridPricesComponent } from 'src/app/components/grid/grid-prices/grid-prices.component';
import { GridHomeAdministratorComponent } from 'src/app/components/grid/grid-home-administrator/grid-home-administrator.component';
import { TableConfigurationsComponent } from 'src/app/components/table/table-configurations/table-configurations.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalAttendOrderComponent } from 'src/app/components/modal/modal-attend-order/modal-attend-order.component';
import { ModalDetailOrderComponent } from 'src/app/components/modal/modal-detail-order/modal-detail-order.component';
import { ModalCancelOrderComponent } from 'src/app/components/modal/modal-cancel-order/modal-cancel-order.component';
import { ModalAddOrderComponent } from 'src/app/components/modal/modal-add-order/modal-add-order.component';
import { ModalEditLocalComponent } from 'src/app/components/modal/modal-edit-local/modal-edit-local.component';
import { ModalAddLocalComponent } from 'src/app/components/modal/modal-add-local/modal-add-local.component';
import { ModalChangePasswordUserComponent } from 'src/app/components/modal/modal-change-password-user/modal-change-password-user.component';
import { ModalEditUserComponent } from 'src/app/components/modal/modal-edit-user/modal-edit-user.component';
import { ModalAddUserComponent } from 'src/app/components/modal/modal-add-user/modal-add-user.component';

@NgModule({
  imports: [
    CommonModule,
    MenuRoutingModule,
    FormsModule,
    NgbModule.forRoot()
  ],
  declarations: [
    MenuComponent,
    HomeComponent,
    SearcherComponent,
    OptionsComponent,
    PricesComponent,
    LocalsComponent,
    UsersComponent,
    ReportsComponent,
    ConfigurationsComponent,
    TableHomeComponent,
    TableSearcherComponent,
    TableLocalsComponent,
    TableUsersComponent,
    TableReportsComponent,
    TableConfigurationsComponent,
    GridHomeComponent,
    GridOptionsComponent,
    GridPricesComponent,
    GridHomeAdministratorComponent,
    ModalAttendOrderComponent,
    ModalDetailOrderComponent,
    ModalCancelOrderComponent,
    ModalAddOrderComponent,
    ModalEditLocalComponent,
    ModalAddLocalComponent,
    ModalChangePasswordUserComponent,
    ModalEditUserComponent,
    ModalAddUserComponent
  ],
  exports:[
    MenuComponent,
    HomeComponent,
    SearcherComponent,
    OptionsComponent,
    PricesComponent,
    LocalsComponent,
    UsersComponent,
    ReportsComponent,
    ConfigurationsComponent,
    TableHomeComponent,
    TableSearcherComponent,
    TableLocalsComponent,
    TableUsersComponent,
    TableReportsComponent,
    TableConfigurationsComponent,
    GridHomeComponent,
    GridOptionsComponent,
    GridPricesComponent,
    GridHomeAdministratorComponent,
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
export class MenuModule { }
