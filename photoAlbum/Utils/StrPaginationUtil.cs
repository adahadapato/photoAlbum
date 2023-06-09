using photoAlbum.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace photoAlbum.Utils
{
    public class StrPaginationUtil
    {
        public static string Strip(string strLiteral)
        {
            try
            {
                string newLiteral=string.Empty;
                foreach(var item in strLiteral.Trim().ToCharArray())
                {
                    if (item != '"')
                    {
                        newLiteral += item;
                    }
                }
                return newLiteral;
            }catch(Exception ex)
            {
                SafeGuiWpf.ShowError(ex.Message);
            }
            return strLiteral;
        }
        public static void Display(Label lbl, int TotalRec, int PageSize, ref int PageIndex, ref int TotalPage, bool isLastPage)
        {
            try
            {
                int rem = TotalRec % PageSize;
                TotalPage = (rem > 0) ? (TotalRec / PageSize) + 1 : (TotalRec / PageSize);
                if (TotalPage == 0)
                    TotalPage = 1;

                if (isLastPage)
                    PageIndex = TotalPage;

                string text = string.Format($"Page {PageIndex}/{TotalPage}" );
                SafeGuiWpf.SetText(lbl, text);
            }catch(Exception e)
            {
                SafeGuiWpf.ShowWarning(e.Message);
                string text = string.Format($"Page 1/1");
                SafeGuiWpf.SetText(lbl, text);
            }
           
        }

        public static string Display(int TotalRec, int PageSize, ref int PageIndex, ref int TotalPage, bool isLastPage)
        {
            try
            {
                int rem = TotalRec % PageSize;
                TotalPage = (rem > 0) ? (TotalRec / PageSize) + 1 : (TotalRec / PageSize);
                if (TotalPage == 0)
                    TotalPage = 1;

                if (isLastPage)
                    PageIndex = TotalPage;

               return string.Format($"Page {PageIndex}/{TotalPage}");
               
            }
            catch (Exception e)
            {
                SafeGuiWpf.ShowWarning(e.Message);
                return string.Format($"Page 1/1");
                
            }

        }

        /*  public static void Display(Label lbl, int TotalRec, int PageSize, ref int PageIndex,  bool isLastPage)
          {
              try
              {
                  int rem = TotalRec % PageSize;
                  var TotalPage = (rem > 0) ? (TotalRec / PageSize) + 1 : (TotalRec / PageSize);
                  if (TotalPage == 0)
                      TotalPage = 1;

                  if (isLastPage)
                      PageIndex = TotalPage;

                  string text = string.Format("Page {0}/{1}", PageIndex, TotalPage);
                  SafeGuiWpf.SetText(lbl, text);
              }
              catch (Exception e)
              {
                  SafeGuiWpf.ShowWarning(e.Message);
                  string text = string.Format("Page {0}/{1}", 1, 1);
                  SafeGuiWpf.SetText(lbl, text);
              }

          }*/
    }
}
