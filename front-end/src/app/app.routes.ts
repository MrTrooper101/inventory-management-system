import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { PasswordSetupComponent } from './features/auth/components/password-setup/password-setup.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
import { ProductsComponent } from './features/products/components/products.component';
import { LayoutComponent } from './shared/layout/layout.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'password-setup', component: PasswordSetupComponent },
    {
        path: '',
        component: LayoutComponent,
        children: [ 
            {
                path: 'dashboard',
                component: DashboardComponent,
                canActivate: [AuthGuard]
            },
            {
                path: 'products',
                component: ProductsComponent,
                canActivate: [AuthGuard]
            },
        ],
    },
    { path: '**', redirectTo: 'login', pathMatch: 'full' },
];