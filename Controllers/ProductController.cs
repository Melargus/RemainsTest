// Decompiled with JetBrains decompiler
// Type: RemainsTest.Controllers.ProductController
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using RemainsTest.Models;
using RemainsTest.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace RemainsTest.Controllers
{
  public class ProductController : Controller
  {
    private DataContext dc = new DataContext();

    [Authorize]
    public ActionResult List(string Search, int groupId)
    {
      this.Session.Add(nameof (Search), (object) (Search ?? ""));
      this.Session.Add(nameof (groupId), (object) groupId);
      IEnumerable<Product> products1 = (IEnumerable<Product>) new List<Product>();
      IEnumerable<Product> products2;
      if (Search != null)
      {
        IQueryable<Product> queryable;
        if (groupId == 0)
          queryable = this.dc.Products.Where<Product>((Expression<Func<Product, bool>>) (p => p.ProductArticle.Contains(Search)));
        else
          queryable = this.dc.Products.Where<Product>((Expression<Func<Product, bool>>) (p => p.ProductGroup == groupId && p.ProductArticle.Contains(Search)));
        products2 = (IEnumerable<Product>) queryable;
      }
      else if ((uint) groupId > 0U)
        products2 = (IEnumerable<Product>) this.dc.Products.Where<Product>((Expression<Func<Product, bool>>) (p => p.ProductGroup == groupId));
      else
        products2 = (IEnumerable<Product>) this.dc.Products;
      return (ActionResult) this.View((object) products2);
    }

    [Authorize]
    public ActionResult Details(int Id)
    {
      ProductDetailsModel productDetailsModel = new ProductDetailsModel();
      productDetailsModel.ProductCharsList = new List<ProductCharModel>();
      productDetailsModel.Parent = this.dc.Products.Where<Product>((Expression<Func<Product, bool>>) (p => p.Id == Id)).FirstOrDefault<Product>();
      ProductPrice productPrice = this.dc.ProductPrice.Where<ProductPrice>((Expression<Func<ProductPrice, bool>>) (pr => pr.ProductId == Id)).SingleOrDefault<ProductPrice>();
      if (productPrice != null)
        productDetailsModel.Price = productPrice.Price;
      IQueryable<ProductChars> source1 = this.dc.ProductChars.Where<ProductChars>((Expression<Func<ProductChars, bool>>) (pch => pch.ProductId == Id));
      if (source1.Any<ProductChars>())
      {
        foreach (ProductChars productChars in (IEnumerable<ProductChars>) source1)
        {
          ProductChars ch = productChars;
          ProductCharModel productCharModel = new ProductCharModel();
          productCharModel.StockList = new List<StockModel>();
          productCharModel.Characteristic = ch;
          IQueryable<ProductCharRemains> source2 = new DataContext().ProductCharRemains.Where<ProductCharRemains>((Expression<Func<ProductCharRemains, bool>>) (pcr => pcr.ProductId == ch.ProductId && pcr.ProductCharId == ch.Id));
          if (source2.Any<ProductCharRemains>())
          {
            productCharModel.TotalRemains = source2.Sum<ProductCharRemains>((Expression<Func<ProductCharRemains, double>>) (x => x.Remains));
            foreach (ProductCharRemains productCharRemains in (IEnumerable<ProductCharRemains>) source2)
            {
              ProductCharRemains shk = productCharRemains;
              StockModel stockModel1 = productCharModel.StockList.Find((Predicate<StockModel>) (x => x.CStock.Id == shk.StockId));
              if (stockModel1 == null)
              {
                StockModel stockModel2 = new StockModel();
                stockModel2.ShkList = new List<ShkModel>();
                DataContext dataContext = new DataContext();
                stockModel2.CStock = dataContext.Stocks.Where<Stock>((Expression<Func<Stock, bool>>) (s => s.Id == shk.StockId)).FirstOrDefault<Stock>();
                stockModel2.ShkList.Add(new ShkModel()
                {
                  Shk = shk.Shk,
                  Remains = shk.Remains
                });
                productCharModel.StockList.Add(stockModel2);
              }
              else
                stockModel1.ShkList.Add(new ShkModel()
                {
                  Shk = shk.Shk,
                  Remains = shk.Remains
                });
            }
          }
          productDetailsModel.ProductCharsList.Add(productCharModel);
        }
      }
      return (ActionResult) this.View((object) productDetailsModel);
    }
  }
}
