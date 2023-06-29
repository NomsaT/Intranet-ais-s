import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DefaultStoresComponent } from './default/default-stores.component';

const routes: Routes = [
    {
        path: 'default',
    component: DefaultStoresComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardsStoresRoutingModule {}
