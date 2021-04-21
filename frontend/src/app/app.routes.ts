import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './dashboard/home/home.component';
import { ProfileComponent } from './dashboard/profile/profile.component';
import { TableComponent } from './dashboard/table/table.component';
import { NotificationComponent } from './dashboard/notification/notification.component';
import { SweetAlertComponent } from './dashboard/sweetalert/sweetalert.component';
import { SettingsComponent } from './dashboard/settings/settings.component';
import { PriceTableComponent } from './dashboard/component/pricetable/pricetable.component';
import { PanelsComponent } from './dashboard/component/panels/panels.component';
import { WizardComponent } from './dashboard/component/wizard/wizard.component';
import { TestTableComponent } from "./dashboard/test-table/test-table.component";

import { RootComponent } from './dashboard/root/root.component';
import { LoginComponent } from './page/login/login.component';
import { RegisterComponent } from './page/register/register.component';
import { AuthGuardService } from './services/authGuard.service';
import { TestDetailComponent } from './dashboard/test-detail/test-detail.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  {
    path: 'dashboard', component: RootComponent,canActivate: [AuthGuardService],
     children: [
      { path: '', component: HomeComponent },
      { path: 'tests', component: TestTableComponent},
      { path: 'tests/testDetail/:testId',component:TestDetailComponent},
      { path: 'profile', component: ProfileComponent },
      { path: 'table', component: TableComponent },
      { path: 'notification', component: NotificationComponent },
      { path: 'alert', component: SweetAlertComponent },
      { path: 'settings', component: SettingsComponent },
      { path: 'components/price-table', component: PriceTableComponent },
      { path: 'components/panels', component: PanelsComponent },
      { path: 'components/wizard', component: WizardComponent }
    ]
  }
];

export const routing = RouterModule.forRoot(routes);

