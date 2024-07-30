///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using System.Transactions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Shared.Extensions;

namespace Youbiquitous.Minimo.Persistence.Repositories;

public class BaseRepository
{
    /// <summary>
    /// Standard way of saving and dealing with errors
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static CommandResponse SaveAndReturn(MinimoDatabase db)
    {
        try
        {
            db.SaveChanges();
            return CommandResponse.Ok().AddMessage(AppErrors.Msg_SuccessfullyDone);
        }
        catch (Exception ex)
        {
            return CommandResponse.Fail()
                .AddMessage(AppErrors.Err_OperationFailed)
                .AddExtra(ex.FullMessage());
        }
    }

    /// <summary>
    /// Standard way of saving and dealing with errors and committing txs
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public static CommandResponse SaveAndReturn(TransactionScope scope, MinimoDatabase db)
    {
        try
        {
            db.SaveChanges();
            scope.Complete();
            return CommandResponse.Ok().AddMessage(AppErrors.Msg_SuccessfullyDone);
        }
        catch (Exception ex)
        {
            return CommandResponse.Fail()
                .AddMessage(AppErrors.Err_OperationFailed)
                .AddExtra(ex.FullMessage());
        }
    }

    /// <summary>
    /// DB operation just completed successfully
    /// </summary>
    /// <returns></returns>
    public static CommandResponse Saved()
    {
        return CommandResponse.Ok().AddMessage(AppErrors.Msg_SuccessfullyDone);
    }

    /// <summary>
    /// DB operation just failed with given exception
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static CommandResponse Failed(Exception ex)
    {
        return CommandResponse.Fail()
            .AddMessage(AppErrors.Err_OperationFailed)
            .AddExtra(ex.FullMessage());
    }
}