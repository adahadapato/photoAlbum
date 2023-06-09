

namespace photoAlbum.DB
{
    using System;
    using photoAlbum.DB.Dal;
    public enum GrConnector
    {
        AccessDal,
        SqlDal,
        FoxProDal,
        OnlineDal
    }

    public abstract class FoxProDalFactory
    {
        public static IGRFoxProDal GetDal(GrConnector connector)
        {
            switch (connector)
            {
                case GrConnector.FoxProDal:
                    return (FoxProDatatAccessLayer)Activator.CreateInstance(typeof(FoxProDatatAccessLayer), true);

                default:
                    return null;
            }
        }
    }
}
