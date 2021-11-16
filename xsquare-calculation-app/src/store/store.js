import axios from 'axios';
import router from '../router';
import { createStore } from 'vuex';

const store = createStore({
    state() {
        return {
            idToken: null,
            userId: null,
            expiredDateTime: null,
            isResponse: false,
            responseTitle: "",
            responseMessage: "",
            responseClass: {
                "bg-green-100": false,
                "border-green-500": false,
                "text-green-700": false,
                "bg-red-100": false,
                "border-red-500": false,
                "text-red-700": false,
                "sticky": false,
                "bottom-2.5": false,
            },
            errors: []
        }
    },
    getters: {
        idToken: state => state.idToken,
        userId: state => state.userId,
        expiredDateTime: state => state.expiredDateTime,
        isResponse: state => state.isResponse,
        responseTitle: state => state.responseTitle,
        responseMessage: state => state.responseMessage,
        responseClass: state => state.responseClass,
        errors: state => state.errors
    },
    mutations: {
        updateIdToken(state, data) {
            state.idToken = data.idToken;
            state.userId = data.userId;
            state.expiredDateTime = data.expiredDateTime;
        },
        updateAlert(state, data) {
            state.isResponse = data.isResponse;
            state.responseTitle = data.responseTitle;
            state.responseMessage = data.responseMessage;
            state.responseClass = data.responseClass;
        },
        addError(state, data) {
            state.errors = data.errors;
        },
        refleshErrors(state) {
            state.errors = [];
        }
    },
    actions: {
        autoLogin({ commit }) {
            const idToken = localStorage.getItem('idToken');
            const userId = localStorage.getItem('userId');
            if (!idToken) return;

            const expiredDateTime = localStorage.getItem('expiredDateTime');
            const isExpired = new Date() >= new Date(expiredDateTime);
            if (isExpired) {
                router.replace('/user/login');
            } else {
                commit('updateIdToken', { idToken: idToken, userId: userId, expiredDateTime: expiredDateTime })
            }
        },
        refleshErrors({ commit }) {
            commit('refleshErrors');
        },
        addError({ commit }, data){
            commit('addError', data);
        },
        login({ commit }, authData) {
            return new Promise((resolve, reject) => {
                axios
                    .post('/login', {
                        userName: authData.userName,
                        userPassword: authData.userPassword
                    })
                    .then(function (response) {
                        commit('updateIdToken', response.data);
                        localStorage.setItem('idToken', response.data.idToken);
                        localStorage.setItem('userId', response.data.userId);
                        localStorage.setItem('expiredDateTime', response.data.expiredDateTime);
                        commit('updateAlert', {
                            isResponse: true,
                            responseTitle: 'Success!!',
                            responseMessage: 'ログインしました！！',
                            responseClass: {
                                "bg-green-100": true,
                                "border-green-500": true,
                                "text-green-700": true,
                                "bg-red-100": false,
                                "border-red-500": false,
                                "text-red-700": false,
                            },
                        })
                        resolve(response)
                        router.replace({ name: 'home', params: { login: 'success' } });
                    })
                    .catch(function (error) {
                        let errors = [];
                        if (error.response.data.content === 'LoginFailed') {
                            errors.push(error.response.data.message);
                        }
                        commit('updateAlert', {
                            isResponse: true,
                            responseTitle: 'Fail...',
                            responseMessage: 'ログイン失敗しました...ごめんなさい...',
                            responseClass: {
                                "bg-green-100": false,
                                "border-green-500": false,
                                "text-green-700": false,
                                "bg-red-100": true,
                                "border-red-500": true,
                                "text-red-700": true,
                            }
                        });

                        commit('addError', {
                            errors: errors
                        });
                        reject();
                    })
            })
        },
        logout({ commit }) {
            if (!confirm("ログアウトしてもよろしいですか？")) return;

            commit('updateIdToken', { idToken: null, userId: null, expiredDateTime: null });
            commit('updateAlert', { isResponse: false, responseTitle: '', responseMessage: '', responseClass: {} });
            localStorage.removeItem('idToken');
            localStorage.removeItem('userId');
            localStorage.removeItem('expiredDateTime');
            router.replace('/');
        },
        createTemplate({ commit }, formData) {
            axios
                .post("/template", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                        Authorization: this.state.idToken,
                        UserId: this.state.userId,
                    },
                })
                .then(() => {
                    commit('updateAlert', {
                        isResponse: true,
                        responseTitle: 'Success!!',
                        responseMessage: '登録完了しました！！',
                        responseClass: {
                            "bg-green-100": true,
                            "border-green-500": true,
                            "text-green-700": true,
                            "bg-red-100": false,
                            "border-red-500": false,
                            "text-red-700": false,
                            "sticky": true,
                            "bottom-2.5": true
                        },
                    })
                    router.replace({ name: 'template-index', params: { createTemplate: 'success' } });
                })
                .catch((error) => {
                    let errors = [];
                    if(error.response.data.content === 'ThumbNailFormatError'){
                        errors.push(error.response.data.message);
                    }

                    if(error.response.data.content === 'TemplateFormatError'){
                        errors.push(error.response.data.message);
                    }

                    commit('updateAlert', {
                        isResponse: true,
                        responseTitle: 'Fail...',
                        responseMessage: '登録失敗しました...ごめんなさい...',
                        responseClass: {
                            "bg-green-100": false,
                            "border-green-500": false,
                            "text-green-700": false,
                            "bg-red-100": true,
                            "border-red-500": true,
                            "text-red-700": true,
                        },
                    });

                    commit('addError', {
                        errors: errors
                    });
                });
        },
        createUser({ commit }) {
            commit('updateAlert', {
                isResponse: true,
                responseTitle: 'Success!!',
                responseMessage: 'ユーザ登録が完了しました！！',
                responseClass: {
                    "bg-green-100": true,
                    "border-green-500": true,
                    "text-green-700": true,
                    "bg-red-100": false,
                    "border-red-500": false,
                    "text-red-700": false,
                },
            })
        },
        failCreateUser({ commit }) {
            commit('updateAlert', {
                isResponse: true,
                responseTitle: 'Fail...',
                responseMessage: 'ユーザ登録に失敗しました...ごめんなさい...',
                responseClass: {
                    "bg-green-100": false,
                    "border-green-500": false,
                    "text-green-700": false,
                    "bg-red-100": true,
                    "border-red-500": true,
                    "text-red-700": true,
                },
            })
        }
    }
});

export default store;