<template>
  <div
    class="
      border-l-4
      p-4
      mt-12
      max-w-xl
      mx-auto
      animate__animated animate__fadeInUp
    "
    :class="responseClass"
    v-if="isResponse"
    role="alert"
  >
    <p class="font-bold">{{ responseTitle }}</p>
    <p>{{ responseMessage }}</p>
  </div>
</template>

<script>
export default {
  computed: {
    isResponse: function () {
      return this.$store.getters.isResponse;
    },
    responseTitle: function () {
      return this.$store.getters.responseTitle;
    },
    responseMessage: function () {
      return this.$store.getters.responseMessage;
    },
    responseClass: function () {
      return {
        "bg-green-100": this.$store.getters.responseClass["bg-green-100"],
        "border-green-500": this.$store.getters.responseClass["border-green-500"],
        "text-green-700": this.$store.getters.responseClass["text-green-700"],
        "bg-red-100": this.$store.getters.responseClass["bg-red-100"],
        "border-red-500": this.$store.getters.responseClass["border-red-500"],
        "text-red-700": this.$store.getters.responseClass["text-red-700"],
        "sticky": this.$store.getters.responseClass["sticky"],
        "bottom-2.5": this.$store.getters.responseClass["bottom-2.5"]
      };
    },
  },
};
</script>