/**
 * @jest-environment jsdom
*/

import { mount, RouterLinkStub } from '@vue/test-utils';
import Header from "@/components/Headers/Header";
import { createStore } from 'vuex';

describe('画面ごとに適切なヘッダーが表示されているか', () => {
    test('ホーム画面: 未ログイン時', function () {
        const store = createStore({
            state() {
                return {
                    idToken: null,
                }
            },
            getters: {
                idToken: state => state.idToken
            },
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/'
                    },
                    $store: store
                }
            }
        });
        const ulElements = wrapper.findAll('ul');
        const liElements = wrapper.findAll('li');
        expect(ulElements).toHaveLength(1)
        expect(liElements[0].text()).toBe('ユーザ登録')
        expect(liElements[1].text()).toBe('ログイン')
    });

    test('ホーム画面: ログイン時', function () {
        const store = createStore({
            state() {
                return {
                    idToken: "12345",
                }
            },
            getters: {
                idToken: state => state.idToken
            },
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/'
                    },
                    $store: store
                }
            }
        });
        const ulElements = wrapper.findAll('ul');
        const liElements = wrapper.findAll('li');
        expect(ulElements).toHaveLength(1)
        expect(liElements[0].text()).toBe('テンプレートを登録')
        expect(liElements[1].text()).toBe('ログアウト')
    })

    test('ホーム画面以外: ログイン時', function () {
        const store = createStore({
            state() {
                return {
                    idToken: "12345",
                }
            },
            getters: {
                idToken: state => state.idToken
            },
        });
        let wrapper = mount(Header, {
            mocks: {
                $route: {
                    path: '/user/create'
                },
            },
            global: {
                mocks: {
                    $route: {
                        path: '/user/create'
                    },
                    $store: store
                }
            }
        });

        const ulElements = wrapper.findAll('ul');
        const liElements = wrapper.findAll('li');
        expect(ulElements).toHaveLength(2);
        expect(liElements[0].text()).toBe('Xマス計算ダウンロードアプリ')
        expect(liElements[1].text()).toBe('テンプレートを登録')
        expect(liElements[2].text()).toBe('ログアウト')
    })
});

describe('画面の遷移先は適切か', () => {
    test('ホーム画面: 未ログイン時: ユーザ登録画面, ログイン画面への遷移', function () {
        const store = createStore({
            state() {
                return {
                    idToken: null,
                }
            },
            getters: {
                idToken: state => state.idToken
            },
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/'
                    },
                    $store: store
                },
                stubs: {
                    RouterLink: RouterLinkStub
                }
            }
        });

        expect(wrapper.findAllComponents(RouterLinkStub).at(0).props().to).toBe('/user/create')
        expect(wrapper.findAllComponents(RouterLinkStub).at(1).props().to).toBe('/user/login')
    });

    test('ホーム画面: ログイン時: テンプレート登録画面への遷移', function () {
        const store = createStore({
            state() {
                return {
                    idToken: "12345",
                }
            },
            getters: {
                idToken: state => state.idToken
            },
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/'
                    },
                    $store: store
                },
                stubs: {
                    RouterLink: RouterLinkStub
                }
            }
        });

        expect(wrapper.findAllComponents(RouterLinkStub).at(0).props().to).toBe('/template/create')
    });

    test('ホーム画面以外: ログイン時: ホーム画面, テンプレート登録画面への遷移', function () {
        const store = createStore({
            state() {
                return {
                    idToken: "12345",
                }
            },
            getters: {
                idToken: state => state.idToken
            },
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/test'
                    },
                    $store: store
                },
                stubs: {
                    RouterLink: RouterLinkStub
                }
            }
        });

        expect(wrapper.findAllComponents(RouterLinkStub).at(0).props().to).toBe('/')
        expect(wrapper.findAllComponents(RouterLinkStub).at(1).props().to).toBe('/template/create')
    });
});

describe('ログアウトボタンが呼ばれるか', () => {
    test('ホーム画面でログアウトボタンをクリック', function () {
        const logoutMock = jest.fn();
        const store = createStore({
            state() {
                return {
                    idToken: "12345",
                }
            },
            getters: {
                idToken: state => state.idToken
            },
            actions: {
                logout() {
                    logoutMock.mockReturnValueOnce('test')
                }
            }
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/'
                    },
                    $store: store
                }
            }
        });
        wrapper.findAll('li').at(-1).trigger('click')
        expect(logoutMock()).toBe('test');
    });

    test('ホーム画面以外でログアウトボタンをクリック', function () {
        const logoutMock = jest.fn();
        const store = createStore({
            state() {
                return {
                    idToken: "12345",
                }
            },
            getters: {
                idToken: state => state.idToken
            },
            actions: {
                logout() {
                    logoutMock.mockReturnValueOnce('test')
                }
            }
        });
        const wrapper = mount(Header, {
            global: {
                mocks: {
                    $route: {
                        path: '/test'
                    },
                    $store: store
                }
            }
        });
        wrapper.findAll('li').at(-1).trigger('click')
        expect(logoutMock()).toBe('test');
    });
});