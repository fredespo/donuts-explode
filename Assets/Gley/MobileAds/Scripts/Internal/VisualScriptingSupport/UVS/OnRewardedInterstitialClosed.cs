#if GLEY_UVS_SUPPORT
using System;
using Unity.VisualScripting;

namespace Gley.MobileAds.Internal
{
    [UnitCategory("Events\\Gley\\MobileAds")]
    public class OnRewardedInterstitialClosed : GameObjectEventUnit<bool>
    {
        [DoNotSerialize]
        public ValueOutput complete { get; private set; }

        public override Type MessageListenerType => null;

        protected override string hookName => EventNames.OnRewardedInterstitialClosed;

        protected override void Definition()
        {
            base.Definition();
            complete = ValueOutput<bool>(nameof(complete));
        }

        protected override void AssignArguments(Flow flow, bool data)
        {
            base.AssignArguments(flow, data);
            flow.SetValue(complete, data);
        }
    }
}
#endif