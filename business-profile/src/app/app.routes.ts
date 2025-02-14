import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { ServicesPageComponent } from './services-page/services-page.component';
import { ContactPageComponent } from './contact-page/contact-page.component';

export const routes: Routes = [
    {path: 'home', component: HomePageComponent},
    {path: 'services', component: ServicesPageComponent},
    {path: 'contact', component: ContactPageComponent}
];
