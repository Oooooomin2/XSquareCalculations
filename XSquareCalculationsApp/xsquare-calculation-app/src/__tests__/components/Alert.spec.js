/**
 * @jest-environment jsdom
*/

import { mount } from '@vue/test-utils';
import Alert from "@/components/Alerts/Alert";
import { createStore } from 'vuex';

describe('アラートの出力値(色やメッセージ)は適切か', () => {
    test('イベント成功時', function () {
        const store = createStore({
            state() {
                return {
                    isResponse: true,
                    responseTitle: '成功',
                    responseMessage: '成功しました',
                    responseClass: {
                        "bg-green-100": true,
                        "border-green-500": true,
                        "text-green-700": true,
                        "bg-red-100": false,
                        "border-red-500": false,
                        "text-red-700": false,
                    },
                }
            },
            getters: {
                isResponse: state => state.isResponse,
                responseTitle: state => state.responseTitle,
                responseMessage: state => state.responseMessage,
                responseClass: state => state.responseClass
            },
        });
        const wrapper = mount(Alert, {
            global: {
                mocks: {
                    $store: store
                }
            }
        });

        expect(wrapper.findAll('p').at(0).text()).toBe('成功');
        expect(wrapper.findAll('p').at(1).text()).toBe('成功しました');
        expect(wrapper.classes()).toContain('bg-green-100');
        expect(wrapper.classes()).toContain('border-green-500');
        expect(wrapper.classes()).toContain('text-green-700');
        
    });

    test('イベント失敗時', function () {
        const store = createStore({
            state() {
                return {
                    isResponse: true,
                    responseTitle: '失敗',
                    responseMessage: '失敗しました',
                    responseClass: {
                        "bg-green-100": false,
                        "border-green-500": false,
                        "text-green-700": false,
                        "bg-red-100": true,
                        "border-red-500": true,
                        "text-red-700": true,
                    },
                }
            },
            getters: {
                isResponse: state => state.isResponse,
                responseTitle: state => state.responseTitle,
                responseMessage: state => state.responseMessage,
                responseClass: state => state.responseClass
            },
        });
        const wrapper = mount(Alert, {
            global: {
                mocks: {
                    $store: store
                }
            }
        });

        expect(wrapper.findAll('p').at(0).text()).toBe('失敗');
        expect(wrapper.findAll('p').at(1).text()).toBe('失敗しました');
        expect(wrapper.classes()).toContain('bg-red-100');
        expect(wrapper.classes()).toContain('border-red-500');
        expect(wrapper.classes()).toContain('text-red-700');
    });
});