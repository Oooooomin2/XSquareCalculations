<template>
  <header class="huifont109">
    <nav class="md:flex px-32">
      <ul
        class="md:flex md:justify-start text-5xl"
        v-if="this.$route.path !== '/'"
      >
        <li class="border-b md:border-none">
          <router-link to="/" class="block px-8 py-4 my-5"
            >Xマス計算ダウンロードアプリ</router-link
          >
        </li>
      </ul>
      <ul class="md:flex md:justify-end text-3xl ml-auto">
        <template v-if="!isAuthenticated">
          <li class="border-b md:border-none">
            <router-link to="/user/create" class="block px-8 py-4 my-5"
              >ユーザ登録</router-link
            >
          </li>
          <li class="border-b md:border-none">
            <router-link to="/user/login" class="block px-8 py-4 my-5"
              >ログイン</router-link
            >
          </li>
        </template>
        <template v-if="isAuthenticated">
          <li class="border-b md:border-none">
            <router-link to="/template/create" class="block px-8 py-4 my-5"
              >テンプレートを登録</router-link
            >
          </li>
          <li class="border-b md:border-none cursor-pointer" @click="logout">
            <div class="block px-8 py-4 my-5">ログアウト</div>
          </li>
        </template>
      </ul>
    </nav>
  </header>
</template>

<script>
export default {
  computed: {
    isAuthenticated() {
      return this.$store.getters.idToken !== null;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
  },
};
</script>

<style scoped>
</style>