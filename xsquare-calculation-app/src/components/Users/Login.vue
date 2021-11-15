<template>
  <div class="huifont109 mt-20">
    <h1 class="text-6xl">ログイン</h1>
    <form class="w-full max-w-xl mx-auto mt-20" @submit.prevent>
      <div
        class="text-red-500 font-bold my-2"
        v-for="error in errors"
        :key="error"
      >
        {{ error }}
      </div>
      <div class="mb-6">
        <div class="md:flex p-3">
          <div class="md:w-1/3">
            <label
              class="block text-gray-500 font-bold mb-1 md:mb-0 pr-3 text-xl"
              for="userName"
            >
              ユーザ名
            </label>
          </div>
          <div class="md:w-2/3">
            <input
              class="
                appearance-none
                border-green-400 border-2
                rounded-lg
                w-full
                py-2
                px-4
                leading-tight
                focus:outline-none focus:border-green-600
                text-xl
              "
              id="userName"
              type="text"
              v-model="userName"
              placeholder="ユーザ名を入力してください。"
            />
          </div>
        </div>
        <div class="md:flex p-3">
          <div class="md:w-1/3">
            <label
              class="block text-gray-500 font-bold mb-1 md:mb-0 pr-3 text-xl"
              for="password"
            >
              パスワード
            </label>
          </div>
          <div class="md:w-2/3">
            <input
              class="
                appearance-none
                border-green-400 border-2
                rounded-lg
                w-full
                py-2
                px-4
                leading-tight
                focus:outline-none focus:border-green-600
                text-xl
                font-sans
              "
              id="userPassword"
              type="password"
              v-model="userPassword"
            />
          </div>
        </div>
      </div>
      <div class="md:flex md:items-center mt-10">
        <input
          @click="login"
          class="
            w-full
            shadow
            bg-green-400
            hover:bg-green-600
            focus:shadow-outline focus:outline-none
            text-white
            font-bold
            text-xl
            py-4
            px-4
            rounded-lg
            cursor-pointer
          "
          type="submit"
          value="ログイン"
        />
      </div>
    </form>
  </div>
</template>


<script>
export default {
  data() {
    return {
      userName: "",
      userPassword: "",
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
    };
  },
  methods: {
    login() {
      this.$store.dispatch("refleshErrors");
      if (!this.userName) {
        this.errors.push("ユーザ名を入力してください。");
      }

      if (!this.userPassword) {
        this.errors.push("パスワードを入力してください。");
      }

      if (this.errors.length > 0) {
        return;
      }

      this.$store.dispatch("login", {
        userName: this.userName,
        userPassword: this.userPassword,
      });
      this.userName = "";
      this.userPassword = "";
    },
  },
  computed: {
    errors: function () {
      return this.$store.getters.errors;
    },
  },
};
</script>

<style scoped>
</style>