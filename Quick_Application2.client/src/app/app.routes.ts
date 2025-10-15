

import { Routes } from '@angular/router';
import { AuthGuard } from './services/auth-guard';


export const routes: Routes = [
  {
    path: '',
    redirectTo: 'figma-home',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent),
    title: 'Login'
  },
  {
    path: 'customers',
    loadComponent: () => import('./components/customers/customers.component').then(m => m.CustomersComponent),
    canActivate: [AuthGuard],
    title: 'Customers'
  },
  {
    path: 'products',
    loadComponent: () => import('./components/products/products.component').then(m => m.ProductsComponent),
    canActivate: [AuthGuard],
    title: 'Products'
  },
  {
    path: 'orders',
    loadComponent: () => import('./components/orders/orders.component').then(m => m.OrdersComponent),
    canActivate: [AuthGuard],
    title: 'Orders'
  },
  {
    path: 'settings',
    loadComponent: () => import('./components/settings/settings.component').then(m => m.SettingsComponent),
    canActivate: [AuthGuard],
    title: 'Settings'
  },
  {
    path: 'about',
    loadComponent: () => import('./components/about/about.component').then(m => m.AboutComponent),
    title: 'About Us'
  },
  {
    path: 'staff',
    loadComponent: () => import('./components/staff/staff.component').then(m => m.StaffComponent),
    title: 'Staff'
  },
  {
    path: 'inmates',
    loadComponent: () => import('./components/inmates/inmates.component').then(m => m.InmatesComponent),
    title: 'inmates'
  },
  {
    path: 'figma-home',
    loadComponent: () =>
      import('./figma-home/figma-home/figma-home.component').then(m => m.FigmaHomeComponent),
    canActivate: [AuthGuard],
    title: 'figma-home'
  },

  {
    path: 'intake',
    loadComponent: () => import('./intake/intake.component').then(m => m.IntakeComponent),
    title: 'intake'
  },
  {
    path: 'medical-intake',
    loadComponent: () => import('./medical-intake/medical-intake.component').then(m => m.MedicalIntakeComponent),
    title: 'medical-intake'
  },

  {
    path: 'home',
    redirectTo: '/',
    pathMatch: 'full'
  },
  {
    path: '**',
    loadComponent: () => import('./components/not-found/not-found.component').then(m => m.NotFoundComponent),
    title: 'Page Not Found'
  }
];
