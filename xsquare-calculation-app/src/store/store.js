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
            },
        }
    },
    getters: {
        idToken: state => state.idToken,
        userId: state => state.userId,
        expiredDateTime: state => state.expiredDateTime,
        isResponse: state => state.isResponse,
        responseTitle: state => state.responseTitle,
        responseMessage: state => state.responseMessage,
        responseClass: state => state.responseClass
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
        }
    },
    actions: {
        autoLogin({ commit }) {
            const idToken = localStorage.getItem('idToken');
            const userId = localStorage.getItem('userId');
            if (!idToken) return;

            const expiredDateTime = localStorage.getItem('expiredDateTime');
            const isExpired = new Date() >= expiredDateTime;
            if (isExpired) {
                router.replace('/user/login');
            } else {
                commit('updateIdToken', { idToken: idToken, userId: userId, expiredDateTime: expiredDateTime })
            }
        },
        login({ commit }, authData) {
            return new Promise((resolve, reject) => {
                axios
                    .post('/login', {
                        userName: authData.userName,
                        userPassword: authData.userPassword
                    })
                    .then(function (response) {
                        if (response.status === 200) {
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
                        }
                    })
                    .catch(function () {
                        console.log('storeのcatch')
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
                            },
                        })
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
                .then((response) => {
                    if (response.status === 200) {
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
                            },
                        })
                        router.replace({ name: 'template-index', params: { createTemplate: 'success' } });
                    } else {
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
                        })
                    }
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
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
                    })
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