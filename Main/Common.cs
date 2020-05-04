using System;
using System.Collections.Generic;
using System.Linq;
using static ClsCommon.Params_Common;
using static ClsCommon.Params_Get;

namespace ClsCommon
{
    public class ClsCommon
    {
        //テスト用の開始コマンド
        static void Main(string[] args)
        {
            var Ret = TestCalc0();
            Console.WriteLine(Ret);
            Console.ReadLine();
        }
        public static string TestCalc0()
        {

            var GetUriList = new List<GetUri>();

            //テスト情報１を作成
            var GetUri1 = new GetUri();
            GetUri1.Add(Joken.Ggenre, ((int)EGenre.ハイファンタジー).ToString());//
            GetUri1.Add(Joken.Gorder, ((int)EOrder.Ghyoka).ToString());//総合ポイントの高い順

            GetUriList.Add(GetUri1);

            //処理開始
            var REFPARAMS = GetMain.GetStart(GetUriList);

            //テスト情報１について・・・
            var i = 0;
            var Ret1 = REFPARAMS.First(t => t.Key == i).Value.ToList()[0].allcount;//１つ目の項目の文字数
            var Ret2 = REFPARAMS.First(t => t.Key == i).Value.ToList()[1].title;//１つ目の項目のタイトル
            var Ret3 = REFPARAMS.First(t => t.Key == i).Value.ToList()[1].story;//１つ目の項目のタイトル

            return Ret1 + "/" + Ret2 + "/" + Ret3;
        }

        //テスト用２
        public static IReadOnlyList<Params_Set> TestCalc()
        {
            var GetUriList = new List<GetUri>();

            //テスト情報１を作成
            var GetUri1 = new GetUri();
            GetUri1.Add(Joken.Ggenre, ((int)EGenre.ハイファンタジー).ToString());//
            GetUri1.Add(Joken.Gorder, ((int)EOrder.Ghyoka).ToString());//総合ポイントの高い順

            GetUriList.Add(GetUri1);

            //処理開始
            var REFPARAMS = GetMain.GetStart(GetUriList);

            return REFPARAMS.First(t => t.Key == 0).Value;
        }
    }
}
