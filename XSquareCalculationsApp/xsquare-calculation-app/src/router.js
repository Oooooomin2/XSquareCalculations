import { createRouter, createWebHistory } from 'vue-router';

import Home from './components/Home/Home'
import TemplateCreate from './components/Templates/Create'
import TemplateIndex from './components/Templates/Index'
import TemplateDetail from './components/Templates/Detail'
import Template from './components/Templates/Template'
import User from './components/Users/User'
import UserCreate from './components/Users/Create'
import UserLogin from './components/Users/Login'

import store from './store/store';

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: Home,
            name: 'home',
            beforeEnter(to) {
                if (to.params.login === 'success') {
                    setTimeout(() => {
                        store.commit('updateAlert', { isResponse: false })
                    }, 1500);
                } else {
                    store.commit('updateAlert', { isResponse: false })
                }
            }
        },
        {
            path: '/template', component: Template, children: [
                {
                    path: 'index',
                    component: TemplateIndex,
                    name: 'template-index',
                    beforeEnter(to) {
                        if (to.params.createTemplate === 'success') {
                            setTimeout(() => {
                                store.commit('updateAlert', { isResponse: false })
                            }, 1500);
                        } else {
                            store.commit('updateAlert', { isResponse: false })
                        }
                    }
                },
                {
                    path: 'create',
                    component: TemplateCreate,
                    beforeEnter(to, from, next) {
                        store.commit('updateAlert', { isResponse: false })
                        if (store.getters.idToken) {
                            next();
                        } else {
                            next('/user/login')
                        }
                    }
                },
                {
                    path: 'detail/:id',
                    component: TemplateDetail,
                    name: "template-detail",
                    props: true,
                    beforeEnter() {
                        store.commit('updateAlert', { isResponse: false })
                    }
                }
            ]
        },
        {
            path: '/user', component: User, children: [
                {
                    path: 'create',
                    component: UserCreate
                },
                {
                    path: 'login',
                    component: UserLogin,
                    props: true
                }
            ]
        },
    ]
});

export default router;