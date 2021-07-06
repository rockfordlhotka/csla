﻿//-----------------------------------------------------------------------
// <copyright file="DataPortalSelector.cs" company="Marimer LLC">
//     Copyright (c) Marimer LLC. All rights reserved.
//     Website: https://cslanet.com
// </copyright>
// <summary>Selects the appropriate data portal implementation</summary>
//-----------------------------------------------------------------------
using System;
using Csla.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Properties;
using System.Threading.Tasks;

namespace Csla.Server
{
  /// <summary>
  /// Selects the appropriate data portal implementation
  /// to invoke based on the object and configuration.
  /// </summary>
  public class DataPortalSelector : IDataPortalServer
  {
    public DataPortalSelector(SimpleDataPortal simpleDataPortal, FactoryDataPortal factoryDataPortal)
    {
      SimpleDataPortal = simpleDataPortal;
      FactoryDataPortal = factoryDataPortal;
    }

    private SimpleDataPortal SimpleDataPortal { get; set; }
    private FactoryDataPortal FactoryDataPortal { get; set; }

    /// <summary>
    /// Create a new business object.
    /// </summary>
    /// <param name="objectType">Type of business object to create.</param>
    /// <param name="criteria">Criteria object describing business object.</param>
    /// <param name="context">
    /// <see cref="Server.DataPortalContext" /> object passed to the server.
    /// </param>
    /// <param name="isSync">True if the client-side proxy should synchronously invoke the server.</param>
    public async Task<DataPortalResult> Create(Type objectType, object criteria, DataPortalContext context, bool isSync)
    {
      try
      {
        context.FactoryInfo = ObjectFactoryAttribute.GetObjectFactoryAttribute(objectType);
        if (context.FactoryInfo == null)
        {
          return await SimpleDataPortal.Create(objectType, criteria, context, isSync).ConfigureAwait(false);
        }
        else
        {
          return await FactoryDataPortal.Create(objectType, criteria, context, isSync).ConfigureAwait(false);
        }
      }
      catch (DataPortalException)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw DataPortal.NewDataPortalException(
          "DataPortal.Create " + Resources.FailedOnServer,
          ex, null);
      }
    }

    /// <summary>
    /// Get an existing business object.
    /// </summary>
    /// <param name="objectType">Type of business object to retrieve.</param>
    /// <param name="criteria">Criteria object describing business object.</param>
    /// <param name="context">
    /// <see cref="Server.DataPortalContext" /> object passed to the server.
    /// </param>
    /// <param name="isSync">True if the client-side proxy should synchronously invoke the server.</param>
    public async Task<DataPortalResult> Fetch(Type objectType, object criteria, DataPortalContext context, bool isSync)
    {
      try
      {
        context.FactoryInfo = ObjectFactoryAttribute.GetObjectFactoryAttribute(objectType);
        if (context.FactoryInfo == null)
        {
          return await SimpleDataPortal.Fetch(objectType, criteria, context, isSync).ConfigureAwait(false);
        }
        else
        {
          return await FactoryDataPortal.Fetch(objectType, criteria, context, isSync).ConfigureAwait(false);
        }
      }
      catch (DataPortalException)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw DataPortal.NewDataPortalException(
          "DataPortal.Fetch " + Resources.FailedOnServer,
          ex, null);
      }
    }

    /// <summary>
    /// Update a business object.
    /// </summary>
    /// <param name="obj">Business object to update.</param>
    /// <param name="context">
    /// <see cref="Server.DataPortalContext" /> object passed to the server.
    /// </param>
    /// <param name="isSync">True if the client-side proxy should synchronously invoke the server.</param>
    public async Task<DataPortalResult> Update(object obj, DataPortalContext context, bool isSync)
    {
      try
      {
        context.FactoryInfo = ObjectFactoryAttribute.GetObjectFactoryAttribute(obj.GetType());
        if (context.FactoryInfo == null)
        {
          return await SimpleDataPortal.Update(obj, context, isSync).ConfigureAwait(false);
        }
        else
        {
          return await FactoryDataPortal.Update(obj, context, isSync).ConfigureAwait(false);
        }
      }
      catch (DataPortalException)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw DataPortal.NewDataPortalException(
          "DataPortal.Update " + Resources.FailedOnServer,
          ex, obj);
      }
    }

    /// <summary>
    /// Delete a business object.
    /// </summary>
    /// <param name="objectType">Type of business object to create.</param>
    /// <param name="criteria">Criteria object describing business object.</param>
    /// <param name="context">
    /// <see cref="Server.DataPortalContext" /> object passed to the server.
    /// </param>
    /// <param name="isSync">True if the client-side proxy should synchronously invoke the server.</param>
    public async Task<DataPortalResult> Delete(Type objectType, object criteria, DataPortalContext context, bool isSync)
    {
      try
      {
        context.FactoryInfo = ObjectFactoryAttribute.GetObjectFactoryAttribute(objectType);
        if (context.FactoryInfo == null)
        {
          return await SimpleDataPortal.Delete(objectType, criteria, context, isSync).ConfigureAwait(false);
        }
        else
        {
          return await FactoryDataPortal.Delete(objectType, criteria, context, isSync).ConfigureAwait(false);
        }
      }
      catch (DataPortalException)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw DataPortal.NewDataPortalException(
          "DataPortal.Delete " + Resources.FailedOnServer,
          ex, null);
      }
    }
  }
}