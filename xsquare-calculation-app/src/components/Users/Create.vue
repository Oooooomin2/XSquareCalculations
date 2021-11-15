<template>
  <div class="huifont109 mt-20">
    <h1 class="text-6xl">ユーザ登録</h1>
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
              id="password"
              type="password"
              v-model="password"
            />
          </div>
        </div>
      </div>
      <div class="md:flex md:items-center mt-10">
        <input
          @click="registUser"
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
          value="登録"
        />
      </div>
    </form>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      userName: "",
      password: "",
      errors: [],
    };
  },
  methods: {
    registUser() {
      this.errors = [];
      if (!this.userName) {
        this.errors.push("ユーザ名を入力してください。");
      }

      if (!this.password) {
        this.errors.push("パスワードを入力してください。");
      }

      if (this.password && this.password.length < 8) {
        this.errors.push("パスワードは8文字以上で入力してください。");
      }

      if (this.errors.length > 0) {
        return;
      }

      let formData = new FormData();
      formData.append("userName", this.userName);
      formData.append("userPassword", this.password);
      axios
        .post("/user", formData)
        .then(() => {
          this.$store.dispatch("createUser");
          this.$router.replace("login");
        })
        .catch((error) => {
          this.$store.dispatch("failCreateUser");
          if (error.response.data.content === "DuplicateUserName") {
            this.errors.push(error.response.data.message);
            return;
          }
        });
    },
  },
};
</script>

<style scoped>
</style>