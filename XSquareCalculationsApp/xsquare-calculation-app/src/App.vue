<template>
  <div class="h-full">
    <headers></headers>
    <router-view></router-view>
    <alert></alert>
  </div>
</template>

<script>
import Headers from "./components/Headers/Header";
import Alert from "./components/Alerts/Alert";

export default {
  name: "App",
  components: {
    Headers,
    Alert
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  height: inherit;
}

body {
  background-image: url(~@/assets/images/background.jpeg);
  background-attachment: fixed;
  overflow-y: scroll;
  background-size: cover;
  height: inherit;
}

html {
  height: 100%;
}

@font-face {
  font-family: "HuiFont109";
  src: url(~@/assets/fonts/HuiFont109.woff) format("woff");
  font-weight: normal;
  font-style: normal;
}

.huifont109 {
  font-family: "HuiFont109", sans-serif;
}
</style>
