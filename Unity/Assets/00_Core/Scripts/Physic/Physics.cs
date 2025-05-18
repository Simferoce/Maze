namespace Game.Core
{
    public static class Physics
    {
        public static bool Overlap(CircleCollision circle, AABBCollision box)
        {
            // Find the closest point on the AABBCollision to the circle's center
            Fixed64 closestX = Math.Max(box.Min.X, Math.Min(circle.Center.X, box.Max.X));
            Fixed64 closestY = Math.Max(box.Min.Y, Math.Min(circle.Center.Y, box.Max.Y));

            // Compute the squared distance between circle center and closest point
            Fixed64 dx = circle.Center.X - closestX;
            Fixed64 dy = circle.Center.Y - closestY;
            Fixed64 distanceSquared = dx * dx + dy * dy;

            // Check if the distance is less than the radius squared
            return distanceSquared <= circle.Radius * circle.Radius;
        }

        public static bool ComputePenetration(CircleCollision circle, AABBCollision box, out Vector2 direction, out Fixed64 depth)
        {
            // Find the closest point on the box to the circle's center
            Fixed64 closestX = Math.Max(box.Min.X, Math.Min(circle.Center.X, box.Max.X));
            Fixed64 closestY = Math.Max(box.Min.Y, Math.Min(circle.Center.Y, box.Max.Y));
            Vector2 closestPoint = new Vector2(closestX, closestY);

            Vector2 offset = circle.Center - closestPoint;
            Fixed64 distanceSquared = offset.SqrMagnitude;
            Fixed64 radius = circle.Radius;

            if (distanceSquared > radius * radius)
            {
                direction = Vector2.Zero;
                depth = Fixed64.Zero;
                return false;
            }

            Fixed64 distance = Math.Sqrt(distanceSquared);

            // Handle case where circle center is inside box (distance == 0)
            if (distance == Fixed64.Zero)
            {
                // Find the minimal axis to push out
                Fixed64 left = circle.Center.X - box.Min.X;
                Fixed64 right = box.Max.X - circle.Center.X;
                Fixed64 bottom = circle.Center.Y - box.Min.Y;
                Fixed64 top = box.Max.Y - circle.Center.Y;

                Fixed64 min = Math.Min(Math.Min(left, right), Math.Min(bottom, top));

                if (min == left)
                    direction = new Vector2(-1, 0);
                else if (min == right)
                    direction = new Vector2(1, 0);
                else if (min == bottom)
                    direction = new Vector2(0, -1);
                else
                    direction = new Vector2(0, 1);

                depth = radius;
            }
            else
            {
                direction = offset.Normalized;
                depth = radius - distance;
            }

            return true;
        }
    }
}
