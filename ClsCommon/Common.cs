using System;
using System.Collections.Generic;
using System.Linq;

namespace ClsCommon
{
    public class Common
    {
        //開始
        public Common()
        {
            var GetUriList = new List<GetUri>();

            //テスト情報１を作成
            var GetUri1 = new GetUri();

            GetUriList.Add(GetUri1);

            //処理開始
            var REFPARAMS = GetMain.GetStart(GetUriList);

            //テスト情報１について・・・
            var i = 0;
            var iCnt1 = REFPARAMS.First( t=> t.Key == i).Value.ToList()[0].allcount;//１つ目の項目の文字数

        }
    }
}
