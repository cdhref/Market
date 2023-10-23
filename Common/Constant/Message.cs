namespace Market.Object.Constant
{
    /// <summary>
    /// システムで使われる文字列を定義するためのClass
    /// </summary>
    public static class Message
    {
        public static readonly string pageKey = "page";
        public static readonly string errNotAvailableParameter = "必需入力項目が入力されておりません。";
        public static readonly string failedUpsert = "入力されたデータの問題、又はシステムエラーで失敗しました。";
        public static readonly string addSuccessMessage = "正常に登録出来ました。";
        public static readonly string modifySuccessMessage = "正常に修正されました。";
        public static readonly string deleteSuccessMessage = "正常に削除されました。";
    }
}
