<template>
  <div class="w-2/3 mt-20 mx-auto huifont109">
    <div class="my-2 text-4xl">テンプレート詳細</div>
    <div class="p-7 bg-white md:flex md:justify-center" v-if="detailData !== null">
      <img
        class="w-1/2"
        :src="'data:image/png;base64,' + detailData.template.thumbNail"
      />
      <div>
        <div class="my-6 text-2xl">
          テンプレート名:
          {{ detailData.template.templateName }}
        </div>
        <div class="my-6 text-2xl">
          制作者:
          {{ detailData.userName }} 様
        </div>
        <div class="my-6 text-2xl">
          ダウンロード数:
          {{ detailData.template.downloadCount }}
        </div>
        <div class="my-6 text-sm">
          作成日時:
          {{ detailData.template.createdTime.replace('T', ' ') }}
        </div>
        <div class="my-6 text-sm">
          更新日時:
          {{ detailData.template.updatedTime.replace('T', ' ') }}
        </div>
        <button
          class="
            bg-green-500
            hover:bg-green-700
            text-white
            font-bold
            py-2
            px-4
            rounded
          "
          @click="downloadTemplate"
        >
          このテンプレートをダウンロードする！
        </button>
      </div>
    </div>
    <router-link
      class="border rounded py-2 px-4 hover:bg-green-100 text-xl"
      to="/template/index"
      >テンプレート一覧へ戻る</router-link
    >
  </div>
</template>

<script>
import axios from "axios";
import { saveAs } from "file-saver";

export default {
  props: ["id"],
  data() {
    return {
      detailData: null,
    };
  },
  created() {
    axios
      .get(`/template/${this.$props.id}`)
      .then((response) => {
        this.detailData = response.data;
      })
      .catch(function (error) {
        console.log(error);
      });
  },
  methods: {
    downloadTemplate() {
      axios
        .get(`/template/${this.detailData.template.templateId}/download`, {
          responseType: "blob",
        })
        .then((response) => {
          const blob = new Blob([response.data], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          });
          saveAs(blob, `${this.detailData.template.templateName}.xlsx`);
          this.detailData.template.downloadCount++;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
};
</script>

<style scoped>
</style>