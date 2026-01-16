namespace P42.Uno.Controls;

/// <summary>
/// Pointer direction extensions.
/// </summary>
public static class PointerDirectionExtensions
{
    extension(PointerDirection dir)
    {
        /// <summary>
        /// Determines if pointer direction is vertical.
        /// </summary>
        /// <returns><c>true</c> if is vertical the specified dir; otherwise, <c>false</c>.</returns>
        public bool IsVertical
            => dir == PointerDirection.Up || dir == PointerDirection.Down;
        
        /// <summary>
        /// Determines if pointer direction is horizontal.
        /// </summary>
        /// <returns><c>true</c> if is horizontal the specified dir; otherwise, <c>false</c>.</returns>
        public bool IsHorizontal
            => dir == PointerDirection.Left || dir == PointerDirection.Right;
        

        /// <summary>
        /// Determines if a pointer is allowed to point left.
        /// </summary>
        /// <returns><c>true</c>, if allowed was lefted, <c>false</c> otherwise.</returns>
        public bool LeftAllowed
            => (dir & PointerDirection.Left) != 0;
        
        /// <summary>
        /// Determines if a pointer is allowed to point right.
        /// </summary>
        /// <returns><c>true</c>, if allowed was righted, <c>false</c> otherwise.</returns>
        public bool RightAllowed
            => (dir & PointerDirection.Right) != 0;
        
        /// <summary>
        /// Determines if a pointer is allowed to point up.
        /// </summary>
        /// <returns><c>true</c>, if allowed was uped, <c>false</c> otherwise.</returns>
        public bool UpAllowed
            => (dir & PointerDirection.Up) != 0;
        
        /// <summary>
        /// Determines if a pointer is allowed to down.
        /// </summary>
        /// <returns><c>true</c>, if allowed was downed, <c>false</c> otherwise.</returns>
        public bool DownAllowed
            => (dir & PointerDirection.Down) != 0;

        /// <summary>
        /// Given the available space, returns the best direction for the pointer.
        /// </summary>
        /// <param name="available"></param>
        /// <returns></returns>
        public PointerDirection BestFitDirection(Thickness available)
        {
            var fitDirections = BestFits(available);
            foreach (var direction in fitDirections)
            {
                if (dir == PointerDirection.None)
                    return direction;
                if ((dir & direction) != 0)
                    return direction;
            }
            return PointerDirection.None;
        }
    }

    /// <summary>
    /// Sorts the available spaces and returns the best fit directions in order.
    /// </summary>
    /// <param name="thickness"></param>
    /// <returns></returns>
    public static IEnumerable<PointerDirection> BestFits(Thickness thickness)
    {

        var fits = new List<BestFitPlace>();
        if (thickness.Left >= 0)
            fits.Add(new BestFitPlace { Space = thickness.Left, Direction = PointerDirection.Right });
        if (thickness.Right >= 0)
            fits.Add(new BestFitPlace { Space = thickness.Right, Direction = PointerDirection.Left });
        if (thickness.Top >= 0)
            fits.Add(new BestFitPlace { Space = thickness.Top, Direction = PointerDirection.Down });
        if (thickness.Bottom >=0)
            fits.Add(new BestFitPlace { Space = thickness.Bottom, Direction = PointerDirection.Up });

        fits = fits.OrderByDescending(place => place.Space).ToList();
        var result = fits.Select(place => place.Direction);
        return result;
    }
}
