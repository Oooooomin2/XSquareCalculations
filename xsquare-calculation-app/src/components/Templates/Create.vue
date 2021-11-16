<template>
  <div class="huifont109 mt-20">
    <h1 class="text-6xl">テンプレート登録</h1>
    <form class="w-full max-w-xl mx-auto mt-20" @submit.prevent="uploadFile">
      <div
        class="text-red-500 font-bold my-2"
        v-for="error in errors"
        :key="error"
      >
        {{ error }}
      </div>
      <div class="md:flex md:items-center mb-6">
        <div class="md:w-1/3">
          <label
            class="block text-gray-500 font-bold mb-1 md:mb-0 pr-3 text-xl"
            for="templateName"
          >
            テンプレート名
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
            id="templateName"
            type="text"
            v-model="templateName"
            placeholder="テンプレート名を入力してください。"
          />
        </div>
      </div>
      <div
        class="flex w-full items-center justify-center bg-grey-lighter mt-10"
      >
        <label
          class="
            w-full
            flex flex-col
            items-center
            px-4
            py-6
            bg-white
            text-blue
            rounded-lg
            shadow-lg
            tracking-wide
            uppercase
            border border-green-200
            cursor-pointer
            hover:bg-green-200
          "
        >
          <svg
            class="w-8 h-8"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
          >
            <path
              d="M16.88 9.1A4 4 0 0 1 16 17H5a5 5 0 0 1-1-9.9V7a3 3 0 0 1 4.52-2.59A4.98 4.98 0 0 1 17 8c0 .38-.04.74-.12 1.1zM11 11h3l-4-4-4 4h3v3h2v-3z"
            />
          </svg>
          <span class="mt-2 text-base leading-normal">{{ thumbNailName }}</span>
          <input
            id="thumbNailNameId"
            type="file"
            @change="postThumbNailFile"
            class="hidden"
            accept="image/png"
          />
        </label>
      </div>
      <div
        class="flex w-full items-center justify-center bg-grey-lighter mt-10"
      >
        <label
          class="
            w-full
            flex flex-col
            items-center
            px-4
            py-6
            bg-white
            text-blue
            rounded-lg
            shadow-lg
            tracking-wide
            uppercase
            border border-green-200
            cursor-pointer
            hover:bg-green-200
          "
        >
          <svg
            class="w-8 h-8"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
          >
            <path
              d="M16.88 9.1A4 4 0 0 1 16 17H5a5 5 0 0 1-1-9.9V7a3 3 0 0 1 4.52-2.59A4.98 4.98 0 0 1 17 8c0 .38-.04.74-.12 1.1zM11 11h3l-4-4-4 4h3v3h2v-3z"
            />
          </svg>
          <span class="mt-2 text-base leading-normal">{{ fileName }}</span>
          <input
            type="file"
            @change="postFile"
            class="hidden"
            accept="application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
          />
        </label>
      </div>
      <div class="md:flex md:items-center mt-10">
        <router-link
          to="/template/index"
          class="
            w-full
            shadow
            border border-green-200
            hover:bg-green-200
            focus:shadow-outline focus:outline-none
            text-xl
            py-4
            px-4
            mx-2
            rounded-lg
            cursor-pointer
          "
        >
          一覧へ戻る
        </router-link>
        <input
          @click="upload"
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
            mx-2
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
export default {
  data() {
    return {
      userId: this.$store.getters.userId,
      idToken: this.$store.getters.idToken,
      thumbNailName: "サムネイル画像を選択してください",
      fileName: "ファイルを選択してください",
      templateName: "",
      templateFile: null,
      thumbNailFile: null,
    };
  },
  computed: {
    authData() {
      return {
        userId: this.$store.getters.userId,
        idToken: this.$store.getters.idToken,
      };
    },
    errors: function () {
      return this.$store.getters.errors;
    },
  },
  methods: {
    postThumbNailFile(e) {
      let files = e.target.files;
      if (files[0].type !== "image/png") {
        this.$store.dispatch("addError", {
          errors: ["サムネイルは画像(.png形式)を登録してください。"],
        });
        return;
      }

      if (files[0].size > 2048000) {
        this.$store.dispatch("addError", {
          errors: ["ファイルサイズは2MB以下である必要があります。"],
        });
        return;
      }

      this.$store.dispatch("refleshErrors");
      this.thumbNailFile = files[0];
      this.thumbNailName = files[0].name;
    },
    postFile(e) {
      let files = e.target.files;
      if (
        files[0].type !==
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
      ) {
        this.$store.dispatch("addError", {
          errors: ["テンプレートはExcel(.xlsx)を登録してください。"],
        });
        return;
      }

      if (files[0].size > 2048000) {
        this.$store.dispatch("addError", {
          errors: ["ファイルサイズは2MB以下である必要があります。"],
        });
        return;
      }

      this.$store.dispatch("refleshErrors");
      this.templateFile = files[0];
      this.fileName = files[0].name;
    },
    upload() {
      this.$store.dispatch("refleshErrors");

      if (!this.templateName) {
        this.errors.push("テンプレート名を入力してください。");
      }

      if (!this.templateFile) {
        this.errors.push("テンプレートをアップしてください。");
      }

      if (this.errors.length > 0) {
        return;
      }

      let formData = new FormData();
      formData.append("templateBlob", this.templateFile);
      formData.append("thumbNail", this.thumbNailFile);
      formData.append("templateName", this.templateName);
      this.$store.dispatch("createTemplate", formData);

      this.templateName = "";
      this.fileName = "ファイルを選択してください";
      this.templateFile = null;
      this.thumbNailName = "サムネイル画像を選択してください";
      this.thumbNailFile = null;
    },
  },
};
</script>

<style scoped>
</style>